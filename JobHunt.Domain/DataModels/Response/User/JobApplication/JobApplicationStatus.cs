using Microsoft.AspNetCore.Http;

namespace JobHunt.Domain.DataModels.Response.User.JobApplication
{
    public class JobApplicationStatus
    {
        public int JobApplicationId { get; set; }

        public int ApplicationStatusId { get; set; }

        public string ApplicationStatus { get; set; } = null!;

        public DateOnly? InterviewDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? Description { get; set; }

        public List<ApplicationStatusModel>? StatusLog { get; set; }
    }
}
