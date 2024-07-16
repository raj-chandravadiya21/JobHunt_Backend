using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company
{
    public class CompanyDetailsResponse
    {
        public string ComapanyName { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public DateOnly? EstablishedDate { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        public string Logo {  get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;   

        public string Description { get; set; } = string.Empty;
    }
}