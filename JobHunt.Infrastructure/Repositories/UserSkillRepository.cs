using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserSkillRepository(DefaultdbContext context) : Repository<UserSkill>(context), IUserSkillRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task AddUserSkill(int userId, List<int> skill)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@user_id", userId),
                new("@skill", skill.ToArray())
            };

            var query = "SELECT public.create_user_skills(@user_id, @skill)";
            await _context.Database.ExecuteSqlRawAsync(query, parameter);
        }
    }
}
