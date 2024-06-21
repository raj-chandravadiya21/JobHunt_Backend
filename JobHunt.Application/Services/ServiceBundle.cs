using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Application.Services.UserService;
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
            RegistrationService = new RegistrationService(_unitOfWork, _mapper);
        }
        public IAuthService AuthService { get; private set; }

        public IRegistrationService RegistrationService { get; private set; }
    }
}
