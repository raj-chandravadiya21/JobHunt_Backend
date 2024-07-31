using AutoMapper;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Request.UserRequest.Profile;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.DataModels.Response.Common;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;

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
            CreateMap<UserApplicationResponse, UserApplicationModel>().ReverseMap();
            CreateMap<InterviewDetail, InterviewDetailsRequest>().ReverseMap();
            CreateMap<ApplicationStatusLog, ApplicationStatusModel>().ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => ((ApplicationStatuses)src.StatusId).ToString())).ReverseMap();
            CreateMap<ExpiredJobListResponse, Job>().ReverseMap();
            CreateMap<ChatResponse, ChatModel>().ForMember(dest => dest.Content, opt => opt.Ignore()).ReverseMap();
        }
    }
}


//CreateMap<ApplicationStatusLog, ApplicationStatusModel>()
//            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => ((ApplicationStatus)src.StatusId).ToString()));