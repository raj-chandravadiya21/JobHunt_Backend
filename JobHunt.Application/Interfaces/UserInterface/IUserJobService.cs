using JobHunt.Domain.DataModels.Request.UserRequest.JobApplication;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.User.JobApplication;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IUserJobService
    {
        Task<PaginatedResponse> FilterJobList(JobListRequest model);
    }
}
