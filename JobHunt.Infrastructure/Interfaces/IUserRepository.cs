using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserProfileModel> GetUserProfile(int userId);
    }
}
