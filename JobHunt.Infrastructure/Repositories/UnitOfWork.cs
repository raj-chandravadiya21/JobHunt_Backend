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
        }
        public IAspnetUserRepo AspNetUser { get; private set; }

        public IUserRepo User { get; private set; }

        public IOtpRecordRepo OtpRecord { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
