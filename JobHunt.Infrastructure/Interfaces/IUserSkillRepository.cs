using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUserSkillRepository : IRepository<UserSkill>
    {
        Task AddUserSkill(int userId, List<int> skill);
    }
}
