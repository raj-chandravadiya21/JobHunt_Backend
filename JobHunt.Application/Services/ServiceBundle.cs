using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Application.Services.CompanyService;
using JobHunt.Application.Services.UserService;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace JobHunt.Application.Services
{
    public class ServiceBundle : IServiceBundle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServiceBundle(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            AuthService = new AuthService(_unitOfWork, _emailSender);
            RegistrationService = new RegistrationService(_unitOfWork, _mapper, _contextAccessor);
            CompanyRegistrationService = new CompanyRegistrationService(_unitOfWork, _mapper, _contextAccessor);
            UserProfileService = new UserProfileService(_unitOfWork, _contextAccessor, _mapper);
            JobPostingService = new JobPostingService(_unitOfWork, _contextAccessor, _mapper);
        }
        public IAuthService AuthService { get; private set; }

        public IRegistrationService RegistrationService { get; private set; }

        public ICompanyRegistrationService CompanyRegistrationService { get; private set;}

        public IUserProfileService UserProfileService { get; private set; }

        public IJobPostingService JobPostingService { get; private set;}
    }
}
