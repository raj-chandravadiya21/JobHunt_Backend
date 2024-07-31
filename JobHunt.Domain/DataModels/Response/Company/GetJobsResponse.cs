using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company
{
    public class GetJobsResponse
    {
        public int? JobId { get; set; }

        public string JobName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int? CtcStart { get; set; }

        public int? CtcEnd { get; set; }

        public DateOnly LastDate { get; set; }

        public int? Openings { get; set; }

        public double? Experience { get; set; }

        public List<int> JobSkills { get; set; } = new List<int>();

        public int TotalCount { get; set; }

        public long NoOfApplicant { get; set; }
    }
}
