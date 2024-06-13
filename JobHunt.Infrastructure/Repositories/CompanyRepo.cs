using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;

namespace JobHunt.Infrastructure.Repositories
{
    public class CompanyRepo(DefaultdbContext context) : Repository<Company>(context), ICompanyRepo
    {
    }
}
