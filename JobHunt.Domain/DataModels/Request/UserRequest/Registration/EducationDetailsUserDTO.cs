using JobHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Registration
{
    public class EducationDetailsUserDTO
    {
        public int EducationType { get; set; }

        public string InstitudeName { get; set; } = string.Empty;

        public string Percentage { get; set; } = string.Empty;

        public string Stream { get; set; } = string.Empty;

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public int Degree { get; set; }
    }
}