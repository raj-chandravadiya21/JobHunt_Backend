using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface IJobPostingService
    {
        Task CreateJob(CreateJobRequest model);

        Task<EditJobDetailsResponse> GetEditJobDetails(int jobId);

        Task<PaginatedResponse> GetJobs(FilterJobRequest model);

        Task<List<ExpiredJobListResponse>> GetExpiredJobList();
    }   
}
