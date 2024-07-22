using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Common
{
    public class JobHuntStatistics
    {
        public int TotalCompany {  get; set; }

        public int TotalJobs { get; set; }

        public int PlacedUsers { get; set; }
    }
}
