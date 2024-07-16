using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PdfSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Repositories
{
    public class JobApplicationRepository(DefaultdbContext context) : Repository<JobApplication>(context), IJobApplicationRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task<List<JobSeekerDetailsResponse>> GetApplicantDetails(int companyId, JobSeekerDetailRequest model)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@companyId", companyId),
                new("@jobId", model.JobId),
                new("@applicationStatus", model.ApplicationStatus),
                new("@applicantName", model.ApplicantName),
                new("@current_page_number", model.PageNumber),
                new("@page_size", model.PageSize)
            };

            return await _context.GetJobSeekerDetails.FromSqlRaw("SELECT * FROM get_applicant(@companyId, @jobId, @applicationStatus, @applicantName, @current_page_number, @page_size)", parameter).ToListAsync();
        }
    }
}
