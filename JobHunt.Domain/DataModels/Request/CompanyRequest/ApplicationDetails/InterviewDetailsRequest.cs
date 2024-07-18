using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails
{
    public class InterviewDetailsRequest
    {
        public int ApplicationId { get; set; }

        public DateOnly InterviewDate { get; set; }

        public string Location { get; set;} = string.Empty;

        public TimeOnly StartTime { get; set;}

        public TimeOnly EndTime { get; set;}
    }
}
