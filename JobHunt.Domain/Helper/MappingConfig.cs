using AutoMapper;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.CompanyRequest;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
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
        }
    }
}
