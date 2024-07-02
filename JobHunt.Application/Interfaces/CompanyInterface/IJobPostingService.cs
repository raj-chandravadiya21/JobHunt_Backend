using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface IJobPostingService
    {
        Task CreateJob(CreateJobRequest model);
    }
}
