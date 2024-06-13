using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserRepo(DefaultdbContext context) : Repository<User>(context), IUserRepo
    {
    }
}
