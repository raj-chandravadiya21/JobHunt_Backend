using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Request.UserRequest.JobApplication;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.User.Dashboard;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using System.Numerics;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<EditJobDetailsResponse> GetJobDetails(int jobId);

        Task<List<GetJobsResponse>> GetJobs(int companyId, FilterJobRequest model);

        Task<List<JobListModel>> GetPaginationAndFilterJob(int userId, JobListRequest model);

        Task<List<HighestPaidJobsResponse>> HighestPaidJobs();

        Task<List<HighestPaidJobsResponse>> UserSkilllsJobs(int userId);
    }
}
    