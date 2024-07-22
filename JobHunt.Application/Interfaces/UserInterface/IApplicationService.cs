using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.User.JobApplication;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IApplicationService
    {
        Task ApplyForJob(ApplyJobRequest model);

        Task<PaginatedResponse> UserApplication(UserApplicationsRequest model);

        Task<JobApplicationStatus> JobApplicationStatus(int jobApplicationId);

        Task<ResumeResponse> GetResume(int applicationId);
    }
}
