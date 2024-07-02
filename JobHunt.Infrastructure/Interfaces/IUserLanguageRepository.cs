using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUserLanguageRepository : IRepository<UserLanguage>
    {
        Task AddLanguage(int userId, List<int> language);
    }
}
