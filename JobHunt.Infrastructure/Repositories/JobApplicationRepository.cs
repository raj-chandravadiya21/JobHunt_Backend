using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
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

        public async Task<List<UserApplicationModel>> GetUserApplication(int userId, UserApplicationsRequest model)
        {


            var parameter = new NpgsqlParameter[]
            {
                new("@user_id", NpgsqlDbType.Integer) { Value = userId },
                new("@pageNumber", NpgsqlDbType.Integer) { Value = model.PageNumber },
                new("@data_size", NpgsqlDbType.Integer) { Value = model.PageSize },
                new("@ctc_start", NpgsqlDbType.Integer) { Value = model.CtcStart },
                new("@ctc_end", NpgsqlDbType.Integer) { Value = model.CtcEnd },
                new("@application_status", NpgsqlDbType.Integer) { Value = model.ApplicationStatus },
                new("@job_name", NpgsqlDbType.Varchar) { Value = model.JobName ?? (object)DBNull.Value },
                new("@order_by", NpgsqlDbType.Varchar) { Value = model.OrderBy ?? (object)DBNull.Value },
                new("@direction", NpgsqlDbType.Varchar) { Value = model.Direction ?? (object)DBNull.Value },
                new("@start_date", NpgsqlDbType.Date) { Value = model.StartDate ?? (object)DBNull.Value },
                new("@end_date", NpgsqlDbType.Date) { Value = model.EndDate ?? (object)DBNull.Value },
            };

            return await _context.UserApplicationModel.FromSqlRaw("SELECT * FROM public.get_my_application(@user_id, @pageNumber, @data_size, @ctc_start, @ctc_end, @application_status, @job_name, @order_by, @direction, @start_date, @end_date)", parameter).ToListAsync();
        }
    }
}
