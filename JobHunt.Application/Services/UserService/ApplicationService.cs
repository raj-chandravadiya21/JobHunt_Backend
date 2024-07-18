using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace JobHunt.Application.Services.UserService
{
    public class ApplicationService(IUnitOfWork unitOfWork, IHttpContextAccessor http, IMapper mapper) : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _http = http;
        private readonly IMapper _mapper = mapper;

        public string GetUserId()
        {
            var token = GetTokenFromHeader.GetToken(_http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }

            return Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!)!;
        }

        public async Task ApplyForJob(ApplyJobRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.AspnetuserId.ToString() == GetUserId());

            if ((bool) await _unitOfWork.JobApplication.GetAnyAsync(u => u.UserId == user!.UserId && u.JobId == model.JobId))
            {
                throw new CustomException("You have already applied this Job");
            }

            var current_timestamp = DateTime.Now;

            JobApplication jobApplication = new()
            {
                JobId = model.JobId,
                UserId = user!.UserId,
                Description = model.Description,
                AppliedDate = current_timestamp,
                StatusId = (int)ApplicationStatuses.Applied,
            };

            await _unitOfWork.JobApplication.CreateAsync(jobApplication);
            await _unitOfWork.SaveAsync();

            var timeStamp = current_timestamp.ToString("ddMMyy_hhmmss");

            if (!model.IsUploadFromProfile && model.Resume != null && model.Resume.Length > 0)
            {
                string newFileName = $"{jobApplication.Id}_{timeStamp}_" + model.Resume.FileName;

                var currentDirectory = Directory.GetCurrentDirectory();
                var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
                var filePath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Resumes", newFileName);
                using var stream = System.IO.File.Create(filePath);
                await model.Resume.CopyToAsync(stream);

                jobApplication.Resume = newFileName;
                _unitOfWork.JobApplication.UpdateAsync(jobApplication);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                await GenerateResume(user!.UserId, jobApplication.Id, timeStamp);

                string newFileName = $"{jobApplication.Id}_{timeStamp}_{user!.FirstName}.pdf";

                jobApplication.Resume = newFileName;
                _unitOfWork.JobApplication.UpdateAsync(jobApplication);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task GenerateResume(int userId, int jobApplicationId, string currentTime)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.UserId == userId);
            var listOfExperience = await _unitOfWork.WorkExperiment.WhereList(u => u.UserId == user!.UserId);
            var listOfEduaction = await _unitOfWork.UserEducation.GetUserEducationInformation(user!.UserId);
            var listOfProject = await _unitOfWork.Project.WhereList(u => u.UserId == user!.UserId);
            var skillAndLanguages = await _unitOfWork.UserSkill.GetSkillAndLanguage(user!.UserId);
            var userSocialProfile = await _unitOfWork.UserSocialProfile.GetFirstOrDefaultNullable(u => u.UserId == user!.UserId);

            string newFileName = $"{jobApplicationId}_{currentTime}_{user!.FirstName}.pdf";

            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            var resumesDirectory = Path.Combine(parentDirectory!, "JobHunt.Domain", "Resumes");

            if (!Directory.Exists(resumesDirectory))
            {
                Directory.CreateDirectory(resumesDirectory);
            }

            var filePath = Path.Combine(resumesDirectory, newFileName);
            var phoneIconPath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Icons", "phone.png");
            var emailIconPath = Path.Combine(parentDirectory!,"JobHunt.Domain", "Icons", "email.png");
            var locationIconPath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Icons", "location.png");
            var dateIconPath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Icons", "date.png");
            var dotIconPath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Icons", "dot.png");

            XImage dateIcon = XImage.FromFile(dateIconPath);

            XImage dotIcon = XImage.FromFile(dotIconPath);

            using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            PdfDocument document = new();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            double margin = 40;
            double width = page.Width - 2 * margin;
            double height = page.Height - 2 * margin;

            XRect rect = new(margin, margin, width, height);

            XFont NameFont = new("Arial Bold", 24);
            XFont bodyFont = new("Arial", 10);
            XFont subHeadingFont = new("Arial Bold", 18);
            XFont jobTitleFont = new ("Arial", 16);
            XFont skillFont = new("Arial", 12);

            XBrush brush = XBrushes.Black;
            XBrush blueBrush = XBrushes.SkyBlue;

            XStringFormat format = new();

            gfx.DrawString($"{user!.FirstName} {user!.LastName}", NameFont, brush, rect, format);

            double iconSize = 14;
            double textOffset = 20;
            double iconY = margin + 40;
            double iconX = margin;
            double textY = iconY + (iconSize / 2) - 6;

            XImage phoneIcon = XImage.FromFile(phoneIconPath);
            gfx.DrawImage(phoneIcon, iconX, iconY, iconSize, iconSize);
            gfx.DrawString($"{user!.PhoneNumber}", bodyFont, brush, new XRect(iconX + textOffset, textY, width, height), format);

            double nextIconX = iconX + textOffset + gfx.MeasureString($"{user!.PhoneNumber}", bodyFont).Width + 35;

            XImage emailIcon = XImage.FromFile(emailIconPath);
            gfx.DrawImage(emailIcon, nextIconX, iconY, iconSize, iconSize);
            gfx.DrawString($"{user!.Email}", bodyFont, brush, new XRect(nextIconX + textOffset, textY, width, height), format);

            nextIconX = nextIconX + textOffset + gfx.MeasureString($"{user!.Email}", bodyFont).Width + 35;

            XImage locationIcon = XImage.FromFile(locationIconPath);
            gfx.DrawImage(locationIcon, nextIconX, iconY, iconSize, iconSize);
            gfx.DrawString($"{user!.City}", bodyFont, brush, new XRect(nextIconX + textOffset, textY, width, height), format);

            double experienceY = iconY + 40;
            gfx.DrawString("Experience", subHeadingFont, brush, new XRect(margin, experienceY, width, height), format);

            double lineY = experienceY + 23;
            gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

            double entryY = lineY + 15;
            double spacing = 15;
            double dateIconSize = 12;
            double dotIconSize = 11;

            void CheckForNewPage(ref double currentY)
            {
                if (currentY >= height + margin - 30)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    currentY = margin;
                }
            }

            foreach (var experience in listOfExperience)
            {
                gfx.DrawString($"{experience.JobTitle}", jobTitleFont, brush, new XRect(margin, entryY + 2, width, height), format);
                entryY += spacing + 5;
                CheckForNewPage(ref entryY);

                gfx.DrawString($"{experience.CompanyName}", bodyFont, blueBrush, new XRect(margin, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dateIcon, margin, entryY, dateIconSize, dateIconSize);
                gfx.DrawString($"{experience.StartDate.Year} - {experience.EndDate.Year}", bodyFont, brush, new XRect(margin + textOffset, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dotIcon, margin, entryY, dotIconSize, dotIconSize);
                gfx.DrawString($"{experience.Description}", bodyFont, brush, new XRect(margin + textOffset, entryY, width, height), format);
                entryY += 2 * spacing;
                CheckForNewPage(ref entryY);
            }

            double educationY = entryY + 12;
            gfx.DrawString("Education", subHeadingFont, brush, new XRect(margin, educationY, width, height), format);

            lineY = educationY + 23;
            gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

            entryY = lineY + 15;

            foreach(var education in listOfEduaction)
            {
                gfx.DrawString($"{education.DegreeName}", jobTitleFont, brush, new XRect(margin, entryY + 2, width, height), format);
                entryY += spacing + 5;
                CheckForNewPage(ref entryY);

                gfx.DrawString($"{education.InstituteName}", bodyFont, blueBrush, new XRect(margin, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dateIcon, margin, entryY, dateIconSize, dateIconSize);
                gfx.DrawString($"{education.StartYear} - {education.EndYear}", bodyFont, brush, new XRect(margin + textOffset, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dotIcon, margin, entryY, dotIconSize, dotIconSize);
                gfx.DrawString($"Grade : {education.PercentageGrade}", bodyFont, brush, new XRect(margin+ textOffset, entryY, width, height), format);
                entryY += 2 * spacing;
                CheckForNewPage(ref entryY);
            }

            double projectY = entryY + 12;
            gfx.DrawString("Projects", subHeadingFont, brush, new XRect(margin, projectY, width, height), format);

            lineY = projectY + 23;
            gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

            entryY = lineY + 15;

            foreach(var project in listOfProject)
            {
                gfx.DrawString($"{project.Title}", jobTitleFont, brush, new XRect(margin, entryY + 2, width, height), format);
                entryY += spacing + 5;
                CheckForNewPage(ref entryY);

                gfx.DrawString($"{project.Url}", bodyFont, blueBrush, new XRect(margin, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dateIcon, margin, entryY, dateIconSize, dateIconSize);
                gfx.DrawString($"{project.StartDate.ToString("MMM yyyy")} - {project.EndDate.ToString("MMM yyyy")}", bodyFont, brush, new XRect(margin + textOffset, entryY, width, height), format);
                entryY += spacing;
                CheckForNewPage(ref entryY);

                gfx.DrawImage(dotIcon, margin, entryY, dotIconSize, dotIconSize);
                gfx.DrawString($"{project.Description}", bodyFont, brush, new XRect(margin + textOffset, entryY, width, height), format);
                entryY += 2 * spacing;
                CheckForNewPage(ref entryY);
            }

            CheckForNewPage(ref entryY);

            double skillY = entryY + 12;
            gfx.DrawString("Skills", subHeadingFont, brush, new XRect(margin, skillY, width, height), format);

            lineY = skillY + 23;
            gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

            double skillsMargin = 40;
            double skillsY = lineY + 20; 
            double skillSpacing = 50;
            double lineSpacing = 20;
            double skillColumnWidth = 100;
            double skillOffSet = 10;

            double skillX = margin;
            double currentY = skillsY;

            foreach (var skill in skillAndLanguages.Skills!)
            {
                gfx.DrawImage(dotIcon, skillX, currentY - (dotIconSize / 2), dotIconSize, dotIconSize);
                skillX += dotIconSize + skillOffSet;

                gfx.DrawString($"{skill}", skillFont, brush, new XRect(skillX, currentY - (dotIconSize / 2), skillColumnWidth, height), format);
                skillX += skillColumnWidth;

                if (skillX + skillColumnWidth > page.Width - margin)
                {
                    skillX = margin;
                    currentY += lineSpacing;
                    CheckForNewPage(ref currentY);
                }
            }

            CheckForNewPage(ref currentY);

            double languageY = currentY + 30;

            CheckForNewPage(ref languageY);

            gfx.DrawString("Languages", subHeadingFont, brush, new XRect(margin, languageY, width, height), format);

            lineY = languageY + 23;
            gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

            double languagesY = lineY + 20;
            double languageX = margin;
            currentY = languagesY;

            foreach(var language in skillAndLanguages.Languages!)
            {
                CheckForNewPage(ref currentY);
                gfx.DrawImage(dotIcon, languageX, currentY - (dotIconSize / 2), dotIconSize, dotIconSize);
                languageX += dotIconSize + skillOffSet;

                gfx.DrawString($"{language}", skillFont, brush, new XRect(languageX, currentY - (dotIconSize / 2), skillColumnWidth, height), format);
                languageX += skillColumnWidth;

                if (languageX + skillColumnWidth > page.Width - margin)
                {
                    languageX = margin;
                    currentY += lineSpacing;
                    CheckForNewPage(ref currentY);
                }
            }

            CheckForNewPage(ref currentY);


            if (userSocialProfile != null)
            {
                double socialProfileY = currentY + 30;
                gfx.DrawString("Social Profile", subHeadingFont, brush, new XRect(margin, socialProfileY, width, height), format);

                lineY = socialProfileY + 23;
                gfx.DrawLine(XPens.Black, margin, lineY, margin + width, lineY);

                CheckForNewPage(ref lineY);
                lineY += 20;

                gfx.DrawString($"GitHub : {userSocialProfile.GithubUrl}", skillFont, brush, new XRect(margin, lineY, width, height), format);
                lineY += 15;
                CheckForNewPage(ref lineY);

                gfx.DrawString($"Linkendin : {userSocialProfile.LinkendinUrl}", skillFont, brush, new XRect(margin, lineY, width, height), format);
                lineY += 15;
                CheckForNewPage(ref lineY);

                gfx.DrawString($"Portfolio : {userSocialProfile.WebsiteUrl}", skillFont, brush, new XRect(margin, lineY, width, height), format);
            }

            document.Save(fileStream);
        }

        public async Task<PaginatedResponse> UserApplication(UserApplicationsRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.AspnetuserId.ToString() == GetUserId());

            List<UserApplicationModel> applicationList = await _unitOfWork.JobApplication.GetUserApplication(user!.UserId, model);

            List<UserApplicationResponse> data = _mapper.Map<List<UserApplicationResponse>>(applicationList);

            PaginatedResponse response = new()
            {
                ListOfData = data,
                CurrentPage = model.PageNumber,
                PageSize = model.PageSize
            };

            if(data.Count > 0)
            {
                response.TotalCount = (int)applicationList[0].TotalCount;
            }

            return response;
        }
    }
}
