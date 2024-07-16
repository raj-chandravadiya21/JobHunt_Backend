using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.User.Registration
{
    public class UserDetailsModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EmailId { get; set; }

        public bool IsRegistered { get; set; }
    }
}
