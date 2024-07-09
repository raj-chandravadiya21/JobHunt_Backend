using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Application
{
    public class ApplyJobRequest
    {
        public int JobId { get; set; }

        public string Resume { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
