using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.User.Dashboard
{
    public class HighestPaidJobsResponse
    {
        public int JobId { get; set; }

        public string JobName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int CtcStart { get; set; }

        public int CtcEnd { get; set;}

        public double Experience { get; set; }

        public DateOnly LastDate { get; set; }

        public int Openings {  get; set; }
    }
}
