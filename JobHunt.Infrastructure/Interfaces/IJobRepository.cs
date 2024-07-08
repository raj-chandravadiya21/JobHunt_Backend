using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<EditJobDetailsResponse> GetJobDetails(int jobId);

        Task<List<GetJobsResponse>> GetJobs(int companyId, FilterJobRequest model);
    }
}
    