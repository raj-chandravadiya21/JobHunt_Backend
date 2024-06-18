using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    { 
        public IAspnetUserRepo AspNetUser { get; }

        public IUserRepo User { get; }

        public IOtpRecordRepo OtpRecord { get; }

        public ICompanyRepo Company { get; }

        Task SaveAsync();
    }
}