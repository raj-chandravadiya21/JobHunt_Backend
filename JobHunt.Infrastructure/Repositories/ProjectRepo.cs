using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Repositories
{
    public class ProjectRepo(DefaultdbContext context) : Repository<Project>(context) , IProjectRepo
    {
    }
}
