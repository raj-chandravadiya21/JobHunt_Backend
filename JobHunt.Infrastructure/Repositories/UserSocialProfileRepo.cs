using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserSocialProfileRepo(DefaultdbContext context) : Repository<UserSocialProfile>(context), IUserSocialProfileRepo
    {
    }
}
