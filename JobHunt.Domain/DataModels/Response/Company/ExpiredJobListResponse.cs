using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Company
{
    public class ExpiredJobListResponse
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public string Location { get; set; }    

        public int CtcStart { get; set; }

        public int CtcEnd { get; set; }

        public DateOnly LastDateToApply { get; set; }

        public DateOnly CloseDate { get; set; }
    }
}