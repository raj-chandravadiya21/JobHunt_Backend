using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.CommonInterface;
using JobHunt.Application.Services.CommonServices;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Application.Services.CompanyService;
using JobHunt.Application.Services.UserService;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using JobHunt.Application.Interfaces.ChatInterface;
using JobHunt.Application.Services.ChatService;
using CloudinaryDotNet;

namespace JobHunt.Application.Services
{
    public class ServiceBundle : IServiceBundle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Cloudinary _cloudinary;

        public ServiceBundle(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, IHttpContextAccessor contextAccessor, Cloudinary cloudinary)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _cloudinary = cloudinary;
            AuthService = new AuthService(_unitOfWork, _emailSender);
            RegistrationService = new RegistrationService(_unitOfWork, _mapper, _contextAccessor);
            CompanyRegistrationService = new CompanyRegistrationService(_unitOfWork, _mapper, _contextAccessor);
            UserProfileService = new UserProfileService(_unitOfWork, _contextAccessor, _mapper);
            JobPostingService = new JobPostingService(_unitOfWork, _contextAccessor, _mapper);
            ApplicationService = new ApplicationService(_unitOfWork, _contextAccessor, _mapper);
            UserJobService = new UserJobService(_contextAccessor, _unitOfWork, _mapper);
            ApplicationDetailsService = new ApplicationDetailsService(_contextAccessor, _unitOfWork, _mapper);
            UserDashboardService = new UserDashboardService(_unitOfWork, _contextAccessor);
            CommonService = new CommonService(_unitOfWork, _mapper);
            ChatService = new ChatServices(_unitOfWork, _mapper, _cloudinary);
        }
        public IAuthService AuthService { get; private set; }

        public IRegistrationService RegistrationService { get; private set; }

        public ICompanyRegistrationService CompanyRegistrationService { get; private set;}

        public IUserProfileService UserProfileService { get; private set; }

        public IJobPostingService JobPostingService { get; private set;}

        public IApplicationService ApplicationService { get; private set; }

        public IUserJobService UserJobService { get; private set; }

        public IApplicationDetailsService ApplicationDetailsService { get; private set; }

        public IUserDashboardService UserDashboardService { get; private set; }

        public ICommonService CommonService { get; private set; }

        public IChatService ChatService { get; private set; }
    }
}
