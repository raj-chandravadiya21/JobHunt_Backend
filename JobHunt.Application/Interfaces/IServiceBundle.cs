using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Application.Interfaces.UserInterface;

namespace JobHunt.Application.Interfaces
{
    public interface IServiceBundle
    {
        public IAuthService AuthService { get; }

        public IRegistrationService RegistrationService { get; }

        public ICompanyRegistrationService CompanyRegistrationService { get; }

        public IUserProfileService UserProfileService { get; }

        public IJobPostingService JobPostingService { get; }

        public IApplicationService ApplicationService { get; }

      public IUserJobService UserJobService { get; }
    }
}
