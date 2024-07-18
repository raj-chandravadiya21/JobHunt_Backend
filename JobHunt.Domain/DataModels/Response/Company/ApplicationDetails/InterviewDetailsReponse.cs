using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company.ApplicationDetails
{
    public class InterviewDetailsReponse
    {
        public DateOnly InterviewDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
    }
}
