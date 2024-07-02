using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserLanguageRepository(DefaultdbContext context) : Repository<UserLanguage>(context), IUserLanguageRepository 
    {
        private readonly DefaultdbContext _context = context;
        public async Task AddLanguage(int userId, List<int> language)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@user_id", userId),
                new("@language", language.ToArray()),
            };

            var query = "SELECT FROM public.create_user_languages(@user_id, @language)";
            await _context.Database.ExecuteSqlRawAsync(query, parameter);
        }
    }
}
