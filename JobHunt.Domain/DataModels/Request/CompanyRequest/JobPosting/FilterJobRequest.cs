using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting
{
    public class FilterJobRequest
    {
        public string JobTitle { get; set; } = string.Empty;

        public int CtcStart { get; set; }

        public int CtcEnd { get; set;} 

        public double Experience { get; set; }

        public List<int> Skills { get; set; } = new List<int>();

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
