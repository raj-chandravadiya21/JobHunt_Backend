using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.DataModels.Response.User.Registration;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace JobHunt.Application.Services.UserService
{
    public class RegistrationService(IUnitOfWork _unitOfWork, IMapper _mapper, IHttpContextAccessor http) : IRegistrationService
    {
        public async Task UserProfile(RegistrationUserRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

            User? user = await _unitOfWork.User.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                user!.PhoneNumber = model.Contact;
                user.DateOfBirth = model.DOB;
                user.Gender = model.Gender;
                user.Experience = model.Experience;
                user.Country = model.Country;
                user.State = model.State;
                user.City = model.City;
                user.Address = model.City + ", " + model.State + ", " + model.Country;
                user.Photo = model.Photo;
                user.ModifiedDate = DateTime.Now;

                _unitOfWork.User.UpdateAsync(user);
                await _unitOfWork.SaveAsync();

                await _unitOfWork.UserLanguage.AddLanguage(user.UserId, model.Languages);

                await _unitOfWork.UserSkill.AddUserSkill(user.UserId, model.Skills);

                for (int i = 0; i < model.EducationDetails.Count; i++)
                {
                    UserEducation userEducation = new()
                    {
                        UserId = user.UserId,
                        DegreeId = model.EducationDetails[i].Degree,
                        InstituteName = model.EducationDetails[i].InstituteName,
                        PercentageGrade = model.EducationDetails[i].Percentage,
                        StartYear = model.EducationDetails[i].StartYear,
                        EndYear = model.EducationDetails[i].EndYear,
                        CreatedDate = DateTime.Now,
                    };
                    await _unitOfWork.UserEducation.CreateAsync(userEducation);
                }

                UserSocialProfile profile = new()
                {
                    UserId = user.UserId,
                    LinkendinUrl = model.LinkedInUrl,
                    GithubUrl = model.GithubUrl,
                    WebsiteUrl = model.WebsiteUrl,
                    CreatedDate = DateTime.Now
                };
                await _unitOfWork.UserSocialProfile.CreateAsync(profile);

                for (int i = 0; i < model.Projects.Count; i++)
                {
                    Project project = new()
                    {
                        UserId = user.UserId,
                        Title = model.Projects[i].Title,
                        Url = model.Projects[i].Url,
                        StartDate = model.Projects[i].StartDate,
                        EndDate = model.Projects[i].EndDate,
                        Description = model.Projects[i].Description,
                        CreatedDate = DateTime.Now
                    };
                    await _unitOfWork.Project.CreateAsync(project);
                }

                for (int i = 0; i < model.WorkExperience.Count; i++)
                {
                    WorkExperience workExperience = new()
                    {
                        UserId = user.UserId,
                        JobTitle = model.WorkExperience[i].JobTitle,
                        CompanyName = model.WorkExperience[i].CompanyName,
                        StartDate = model.WorkExperience[i].StartDate,
                        EndDate = model.WorkExperience[i].EndDate,
                        Description = model.WorkExperience[i].JobDescription,
                        CreatedDate = DateTime.Now
                    };
                    await _unitOfWork.WorkExperiment.CreateAsync(workExperience);
                }

                aspnetuser!.IsRegistered = true;
                aspnetuser.ModifiedDate = DateTime.Now;

                await _unitOfWork.SaveAsync();

                transactionScope.Complete();
            }
        }

        public async Task<List<SkillResponse>> GetAllSkill()
        {
            return _mapper.Map<List<SkillResponse>>(await _unitOfWork.Skill.GetAllAsync());
        }

        public async Task<List<LanguageResponse>> GetAllLanguage()
        { 
            return _mapper.Map<List<LanguageResponse>>(await _unitOfWork.Language.GetAllAsync());
        }

        public async Task<List<DegreeTypeResponse>> GetAllDegreeType()
        {
            List<DegreeTypeResponse> SSC =_mapper.Map<List<DegreeTypeResponse>>(_unitOfWork.DegreeType.GetWhere(x=>x.Type == "SSC"));
            List<DegreeTypeResponse> HSC =_mapper.Map<List<DegreeTypeResponse>>(_unitOfWork.DegreeType.GetWhere(x=>x.Type == "HSC"));
            List<DegreeTypeResponse> Bachelor =_mapper.Map<List<DegreeTypeResponse>>(_unitOfWork.DegreeType.GetWhere(x=>x.Type == "Bachelor"));
            List<DegreeTypeResponse> Master =_mapper.Map<List<DegreeTypeResponse>>(_unitOfWork.DegreeType.GetWhere(x=>x.Type == "Master"));

            return SSC.Concat(HSC).Concat(Bachelor).Concat(Master).ToList();
        }

        public async Task<UserDetailsModel> GetUserDetails()
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            var aspNetUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspNetUserId);

            User? user = await _unitOfWork.User.GetFirstOrDefault(x => x.AspnetuserId == aspnetuser!.AspnetuserId);

            UserDetailsModel model = new UserDetailsModel();

            if (aspnetuser.IsRegistered)
            {
                    model.FirstName = user!.FirstName;
                    model.LastName = user.LastName;
                    model.EmailId = user.Email;
                    model.IsRegistered = true;
            }
            else
            {
                model.IsRegistered = false;
            }
            
            return model;
        }
    }
}
