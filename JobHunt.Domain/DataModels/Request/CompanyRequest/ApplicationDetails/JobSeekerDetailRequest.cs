using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails
{
    public class JobSeekerDetailRequest : PaginationParameter
    {
        public int JobId { get; set; }

        public int ApplicationStatus { get; set; }

        public string ApplicantName { get; set; } = string.Empty;
    }
}
