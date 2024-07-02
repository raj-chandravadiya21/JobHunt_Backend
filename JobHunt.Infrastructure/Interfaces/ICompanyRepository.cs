using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<JobDetails> JobDetails(int jobId);
    }
}
