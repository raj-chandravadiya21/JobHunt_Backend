using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Repositories
{
    public class AspnetUserRepo(DefaultdbContext context) : Repository<Aspnetuser>(context), IAspnetUserRepo
    {

    }
}
