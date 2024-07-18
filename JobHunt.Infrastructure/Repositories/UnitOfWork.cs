using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace JobHunt.Infrastructure.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly DefaultdbContext _context;

        public UnitOfWork(DefaultdbContext context)
        {
            _context = context;
            AspNetUser = new Repository<Aspnetuser>(_context);
            User = new UserRepository(_context);
            OtpRecord = new Repository<OtpRecord>(_context);
            UserSkill = new UserSkillRepository(_context);
            UserLanguage = new UserLanguageRepository(_context);
            UserSocialProfile = new Repository<UserSocialProfile>(_context);
            Project = new Repository<Project>(_context);
            UserEducation = new UserEducationRepository(_context);
            WorkExperiment = new Repository<WorkExperience>(_context);
            Skill = new Repository<Skill>(_context);
            Language = new Repository<Language>(_context);
            DegreeType = new Repository<DegreeType>(_context);
            Job = new JobRepository(_context);
            JobSkill = new Repository<JobSkill>(_context);
            JobResponsibility = new Repository<JobResponsibility>(_context);
            JobPerks = new Repository<JobPerk>(_context);
            Company = new CompanyRepository(_context);
            JobApplication = new JobApplicationRepository(_context);
            ApplicationStatusLog = new Repository<ApplicationStatusLog>(_context);
            InterviewDetail = new Repository<InterviewDetail>(_context);
        }
        public IRepository<Aspnetuser> AspNetUser { get; private set; }

        public IRepository<OtpRecord> OtpRecord { get; private set; }

        public IRepository<UserSocialProfile> UserSocialProfile { get; private set; }

        public IRepository<Project> Project { get; private set; }

        public IRepository<WorkExperience> WorkExperiment { get; private set; }

        public IRepository<Skill> Skill { get; private set; }

        public IRepository<Language> Language { get; private set; }

        public IRepository<DegreeType> DegreeType { get; private set; }

        public IRepository<JobSkill> JobSkill { get; private set; }

        public IRepository<JobResponsibility> JobResponsibility { get; private set; }

        public IRepository<JobPerk> JobPerks { get; private set; }

        public IRepository<ApplicationStatusLog> ApplicationStatusLog { get; private set; }

        public IRepository<InterviewDetail> InterviewDetail { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IUserRepository User { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IUserSkillRepository UserSkill { get; private set; }

        public IUserLanguageRepository UserLanguage { get; private set; }

        public IJobRepository Job { get; private set; }

        public IJobApplicationRepository JobApplication { get; private set; }
        
        public IUserEducationRepository UserEducation { get; private set; } 
    }
}
