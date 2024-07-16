using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IUserEducationRepository : IRepository<UserEducation>
    {
        Task<List<UserEducationModel>> GetUserEducationInformation(int userId);
    }
}
