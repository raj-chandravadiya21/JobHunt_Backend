using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserRepository(DefaultdbContext context) : Repository<User>(context), IUserRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task<UserProfileModel> GetUserProfile(int userId)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@user_id",userId),
            };

            var result = await _context.UserProfiles.FromSqlRaw("SELECT * FROM get_user_profile(@user_id)", parameter).FirstAsync();
            return result;
        }
    }
}
