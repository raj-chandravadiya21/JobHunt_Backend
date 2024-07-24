using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting
{
    public class CreateJobRequest
    {
        public string? Name { get; set; }

        public string? Location { get; set; }

        public DateOnly JobStartDate { get; set; }

        public int CTCStart { get; set; }

        public int CTCEnd { get; set; }

        public double Experience { get; set; }

        public DateOnly LastDate { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Openings must be greater than 0")]
        public int NoOfOpening { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }

        public List<int> Skills { get; set; } = new List<int>();

        public List<string> Perks { get; set; } = new List<string>();

        public List<string> Responsibility { get; set; } = new List<string>();
    }
}
