using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company.ApplicationDetails
{
    public class JobSeekerCountWithStatus
    {
        public string JobName { get; set; } = string.Empty;

        public int AppliedCount { get; set; }

        public int UnderReviewCount { get; set; }

        public int ScheduleInterviewCount { get; set; }

        public int InterviewedCount { get; set; }

        public int SelectedCount { get; set; }

        public int RejectedCount { get; set; }
    }
}
