using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace JobHunt.Application.Services
{
    public class ServiceBundle : IServiceBundle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public ServiceBundle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }
        public IAuthService AuthService { get; private set; } 
    }
}
