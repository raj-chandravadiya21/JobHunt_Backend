using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company.ApplicationDetails
{
    public class JobSeekerDetailsModel
    {
        public int UserId { get; set; }

        public int ApplicationId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public DateOnly? AppliedDate { get; set; }

        public string? Resume { get; set; } = string.Empty;

        public string ApplicationStatus { get; set; } = string.Empty;

        public DateOnly? InterviewDate { get; set; }

        public TimeOnly? InterviewStartTime { get; set; }

        public TimeOnly? InterviewEndTime { get; set; }

        public string? Location { get; set; } = string.Empty;

        public List<string>? Notes { get; set; } = new List<string>();

        public int ConversationId { get; set; }

        public string JobName { get; set; } = string.Empty;
    }
}
