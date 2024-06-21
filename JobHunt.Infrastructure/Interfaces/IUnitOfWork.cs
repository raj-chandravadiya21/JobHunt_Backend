using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    { 
        public IAspnetUserRepo AspNetUser { get; }

        public IUserRepo User { get; }

        public IOtpRecordRepo OtpRecord { get; }

        public ICompanyRepo Company { get; }

        public IUserSkillRepo UserSkill { get; }

        public IUserLanguageRepo UserLanguage { get; }

        public IUserSocialProfileRepo UserSocialProfile { get; }

        public IProjectRepo Project { get; }

        public IUserEducationRepo UserEducation { get; }

        public IWorkExperimentRepo WorkExperiment { get; }  
        
        Task SaveAsync();
    }
}