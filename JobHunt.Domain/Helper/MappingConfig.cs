using AutoMapper;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.Entities;

namespace JobHunt.Domain.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Aspnetuser, RegisterUserDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
        }
    }
}
