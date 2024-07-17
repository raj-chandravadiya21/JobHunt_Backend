using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company.ApplicationDetails
{
    public class JobSeekerDetailsResponse
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public DateOnly? AppliedDate { get; set; }   

        public string? Resume { get; set; } = string.Empty;

        public string ApplicationStatus { get; set; } = string.Empty;

        public DateOnly? InterviewDate { get; set; }
        
        public DateTime? InterviewStartTime { get; set; }

        public DateTime? InterviewEndTime { get; set; }

        public string? Location { get; set; } = string.Empty;

        public string? CoverDescription {  get; set; } = string.Empty;

        public List<string>? Notes { get; set; } = new List<string>();

        public int TotalCount { get; set; } = 0;
    }
}