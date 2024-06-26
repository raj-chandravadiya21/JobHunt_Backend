using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    { 
        public IRepository<Aspnetuser> AspNetUser { get; }

        public IRepository<User> User { get; }

        public IRepository<OtpRecord> OtpRecord { get; }

        public IRepository<Company> Company { get; }

        public IRepository<UserSkill> UserSkill { get; }

        public IRepository<UserLanguage> UserLanguage { get; }

        public IRepository<UserSocialProfile> UserSocialProfile { get; }

        public IRepository<Project> Project { get; }

        public IRepository<UserEducation> UserEducation { get; }

        public IRepository<WorkExperience> WorkExperiment { get; }  

        public IRepository<Skill> Skill { get; }

        public IRepository<Language> Language { get; }

        public IRepository<DegreeType> DegreeType { get; }

        Task SaveAsync();
    }
}