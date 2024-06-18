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
        private readonly IEmailSender _emailSender;

        public ServiceBundle(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            AuthService = new AuthService(_unitOfWork, _mapper, _emailSender);
        }
        public IAuthService AuthService { get; private set; }
    }
}
