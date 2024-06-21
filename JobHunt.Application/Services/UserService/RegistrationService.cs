﻿using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace JobHunt.Application.Services.UserService
{
    public class RegistrationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IRegistrationService
    {
        public async Task<ResponseDTO> UserProfile(RegistrationUserDTO model)
        {
            var isValidToken = Jwt.ValidateToken(model.Token!, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                return new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Invalid Token",
                };
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);
            if (aspnetuser != null)
            {
                User? user = await _unitOfWork.User.GetFirstOrDefault(x => x.Email == aspnetuser.Email);

                if (user != null)
                {
                    user.PhoneNumber = model.Contact;
                    user.DateOfBirth = model.DOB;
                    user.Gender = model.Gender;
                    user.Experience = model.Experience;
                    user.Address = model.Address;
                    user.Photo = model.Photo;
                    user.ModifiedDate = DateTime.Now;

                    _unitOfWork.User.UpdateAsync(user);
                    await _unitOfWork.SaveAsync();

                    for (int i = 0; i < model.Skills.Count; i++)
                    {
                        UserSkill userSkill = new()
                        {
                            UserId = user.UserId,
                            SkillId = model.Skills[i]
                        };
                        await _unitOfWork.UserSkill.CreateAsync(userSkill);
                    }

                    for (int i = 0; i < model.Languages.Count; i++)
                    {
                        UserLanguage userLanguage = new()
                        {
                            UserId = user.UserId,
                            LanguageId = model.Languages[i]
                        };
                        await _unitOfWork.UserLanguage.CreateAsync(userLanguage);
                    }

                    for (int i = 0; i < model.EducationDetails.Count; i++)
                    {
                        UserEducation userEducation = new()
                        {
                            UserId = user.UserId,
                            UserEducationTypeId = model.EducationDetails[i].EducationType,
                            DegreeId = model.EducationDetails[i].Degree,
                            InstituteName = model.EducationDetails[i].InstitudeName,
                            PercentageGrade = model.EducationDetails[i].Percentage,
                            Streem = model.EducationDetails[i].Stream,
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

                    aspnetuser.IsRegistered = true;
                    aspnetuser.ModifiedDate = DateTime.Now;

                    await _unitOfWork.SaveAsync();

                    return new()
                    {
                        IsSuccess = true,
                        Message = "User Registration Completed Successfully.",
                        StatusCode = HttpStatusCode.OK,
                    };
                }

                return new()
                {
                    IsSuccess = false,
                    Message = "User Not Found.",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            return new()
            {
                IsSuccess = false,
                Message = "User Not Found.",
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
    }
}
