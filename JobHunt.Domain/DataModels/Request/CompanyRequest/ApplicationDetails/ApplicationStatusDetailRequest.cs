using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails
{
    public class ApplicationStatusDetailRequest
    {
        public int ApplicationId { get; set; }

        public string Notes { get; set; } = null!;
    }
}
