using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.Registration
{
    public class CompanyDetailsModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public bool IsRegistred { get; set; }
    }
}
