﻿using AutoMapper;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Request.UserRequest.Profile;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;

namespace JobHunt.Domain.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Aspnetuser, RegisterUserRequest>().ReverseMap();
            CreateMap<User, RegisterUserRequest>().ReverseMap();
            CreateMap<Skill, SkillResponse>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();  
            CreateMap<DegreeType, DegreeTypeResponse>().ReverseMap();
            CreateMap<UserSocialProfile, UserSocialProfileResponse>().ReverseMap();
            CreateMap<UserEducation, UserEducationResponse>().ReverseMap();
            CreateMap<Project, UserProjectResponse>().ReverseMap();
            CreateMap<WorkExperience, UserWorkExperience>().ReverseMap();
            CreateMap<UserProfileRequest, User>().ReverseMap();
            CreateMap<UpdateWorkExperience, WorkExperience>().ReverseMap();
            CreateMap<AddWorkExperienceRequest, WorkExperience>().ReverseMap();
            CreateMap<UpdateProjectRequest, Project>().ReverseMap();
            CreateMap<AddEducationRequest, UserEducation>().ReverseMap();
            CreateMap<UserSocialProfileRequest, UserSocialProfile>().ReverseMap();
            CreateMap<AddProjectRequest, Project>().ReverseMap();
            CreateMap<JobApplication, ApplyJobRequest>().ReverseMap();
            CreateMap<UpdateEducationRequest, UserEducation>().ReverseMap();
            CreateMap<JobListResponse, JobListModel>().ReverseMap();
            CreateMap<GetJobListResponce, GetJobsResponse>().ReverseMap();
            CreateMap<JobSeekerDetailsResponse, JobSeekerDetailsModel>().ReverseMap();
        }
    }
}
