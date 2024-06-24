using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Registration
{
    public class ProjectsUserModel
    {
        public string Title { get; set; } =string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
