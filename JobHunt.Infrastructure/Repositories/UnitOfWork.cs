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
            AspNetUser = new Repository<Aspnetuser>(_context);
            User = new UserRepository(_context);
            OtpRecord = new Repository<OtpRecord>(_context);
            Company = new Repository<Company>(_context);
            UserSkill = new Repository<UserSkill>(_context);
            UserLanguage = new Repository<UserLanguage>(_context);
            UserSocialProfile = new Repository<UserSocialProfile>(_context);
            Project = new Repository<Project>(_context);
            UserEducation = new Repository<UserEducation>(_context);
            WorkExperiment = new Repository<WorkExperience>(_context);
            Skill = new Repository<Skill>(_context);
            Language = new Repository<Language>(_context);
            DegreeType = new Repository<DegreeType>(_context);
        }
        public IRepository<Aspnetuser> AspNetUser { get; private set; }

        public IUserRepository User { get; private set; }

        public IRepository<OtpRecord> OtpRecord { get; private set; }

        public IRepository<Company> Company { get; private set; }

        public IRepository<UserSkill> UserSkill { get; private set; }

        public IRepository<UserLanguage> UserLanguage { get; private set; } 

        public IRepository<UserSocialProfile> UserSocialProfile { get; private set; }

        public IRepository<Project> Project { get; private set; }

        public IRepository<UserEducation> UserEducation {  get; private set; } 

        public IRepository<WorkExperience> WorkExperiment { get; private set; }

        public IRepository<Skill> Skill { get; private set; }

        public IRepository<Language> Language { get; private set; }

        public IRepository<DegreeType> DegreeType { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
