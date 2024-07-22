using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Infrastructure.Interfaces;
using JobHunt.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHunt.Domain.DataModels.Response.User.Dashboard;

namespace JobHunt.Application.Services.UserService
{
    public class UserDashboardService(IUnitOfWork unitOfWork) : IUserDashboardService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<List<HighestPaidJobsResponse>> HighestPaidJobs()
        {
            return await _unitOfWork.Job.HighestPaidJobs();
        }
    }
}
