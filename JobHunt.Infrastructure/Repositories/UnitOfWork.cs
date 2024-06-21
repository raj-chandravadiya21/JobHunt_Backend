using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using JobHunt.Infrastructure.Interfaces;

namespace JobHunt.Infrastructure.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly DefaultdbContext _context;

        public UnitOfWork(DefaultdbContext context)
        {
            _context = context;
            AspNetUser = new AspnetUserRepo(_context);
            User = new UserRepo(_context);
            OtpRecord = new OtpRecordRepo(_context);
            Company = new CompanyRepo(_context);
            UserSkill = new UserSkillRepo(_context);
            UserLanguage = new UserLanguageRepo(_context);
            UserSocialProfile = new UserSocialProfileRepo(_context);
            Project = new ProjectRepo(_context);
            UserEducation = new UserEducationRepo(_context);
            WorkExperiment = new WorkExperienceRepo(_context);
        }
        public IAspnetUserRepo AspNetUser { get; private set; }

        public IUserRepo User { get; private set; }

        public IOtpRecordRepo OtpRecord { get; private set; }

        public ICompanyRepo Company { get; private set; }

        public IUserSkillRepo UserSkill { get; private set; }

        public IUserLanguageRepo UserLanguage { get; private set; } 

        public IUserSocialProfileRepo UserSocialProfile { get; private set; }

        public IProjectRepo Project { get; private set; }

        public IUserEducationRepo UserEducation {  get; private set; } 

        public IWorkExperimentRepo WorkExperiment { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
