using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Common
{
    public class JobDetails
    {
        public int JobId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string JobName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public int CtcStart { get; set; }

        public int CtcEnd { get; set; }

        public double Experience { get; set; }

        public DateOnly LastDate { get; set; }

        public int NoOfOpening { get; set; }

        public string JobDescription { get; set; } = null!;

        public string JobRequirements { get; set; } = null!;

        public List<string> JobPerks { get; set; } = new List<string>();

        public List<string> JobResponsibility { get; set; } = new List<string>();

        public List<string> JobSkill { get; set; } = new List<string>();
    }
}
