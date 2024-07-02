using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    { 
        public IRepository<Aspnetuser> AspNetUser { get; }

        public IRepository<OtpRecord> OtpRecord { get; }

        public IRepository<UserSocialProfile> UserSocialProfile { get; }

        public IRepository<Project> Project { get; }

        public IRepository<UserEducation> UserEducation { get; }

        public IRepository<WorkExperience> WorkExperiment { get; }  

        public IRepository<Skill> Skill { get; }

        public IRepository<Language> Language { get; }

        public IRepository<DegreeType> DegreeType { get; }

        public IRepository<Job> Job { get; }

        public IRepository<JobSkill> JobSkill { get; }

        public IRepository<JobResponsibility> JobResponsibility { get; }

        public IRepository<JobPerk> JobPerks { get; }

        Task SaveAsync();

        public IUserRepository User { get; }

        public ICompanyRepository Company { get; }
      
        public IUserSkillRepository UserSkill { get; }

        public IUserLanguageRepository UserLanguage { get; }
    }
}