using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;
using JobHunt.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        Task<List<JobSeekerDetailsResponse>> GetApplicantDetails(int companyId, JobSeekerDetailRequest model);

        Task<List<UserApplicationModel>> GetUserApplication(int userId, UserApplicationsRequest model);
    }
}
