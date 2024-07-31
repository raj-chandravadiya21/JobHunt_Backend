using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface IJobPostingService
    {
        Task CreateJob(CreateJobRequest model);

        Task<EditJobDetailsResponse> GetEditJobDetails(int jobId);
        
        Task UpdateJobDetails(UpdateJobRequest model);

        Task<PaginatedResponse> GetJobs(FilterJobRequest model);

        Task<List<ExpiredJobListResponse>> GetExpiredJobList(PaginationParameter model);

        Task<List<ExpiredJobListResponse>> GetClosedJobList(PaginationParameter model);

        Task CloseJob(int jobId);

        Task UpdateExpiredJob(int jobId, DateOnly lastDate);
    }   
}
