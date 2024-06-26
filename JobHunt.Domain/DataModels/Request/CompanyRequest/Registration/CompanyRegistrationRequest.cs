using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.CompanyRequest.Registration
{
    public class CompanyRegistrationRequest
    {
        public string? PhoneNumber { get; set; }

        public string? WebsiteUrl { get; set; }

        public DateOnly EstablishedDate { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public string? Logo { get; set; }
    }
}
