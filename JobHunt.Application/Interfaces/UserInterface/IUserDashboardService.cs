using JobHunt.Domain.DataModels.Response.User.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IUserDashboardService
    {
        Task<List<HighestPaidJobsResponse>> HighestPaidJobs();
    }
}
