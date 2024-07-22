using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Request.UserRequest.JobApplication;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.User.Dashboard;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Numerics;
using System.Text;

namespace JobHunt.Infrastructure.Repositories
{
    public class JobRepository(DefaultdbContext context) : Repository<Job>(context), IJobRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task<EditJobDetailsResponse> GetJobDetails(int jobId)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@jobId", jobId),
            };

            return await _context.EditJobDetailsResponses.FromSqlRaw("SELECT * FROM get_job_details(@jobId)", parameter).FirstAsync();
        }

        public async Task<List<GetJobsResponse>> GetJobs(int companyId, FilterJobRequest model)
        {
            var jobSkillsArray = model.Skills != null ? model.Skills.ToArray() : new int[0];

            var parameter = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@companyId", companyId),
                new NpgsqlParameter("@jobTitle", model.JobTitle ?? (object)DBNull.Value),
                new NpgsqlParameter("@ctcStart", model.CtcStart),
                new NpgsqlParameter("@ctcEnd", model.CtcEnd),
                new NpgsqlParameter("@experience", model.Experience),
                new NpgsqlParameter("@job_skills", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = jobSkillsArray },
                new NpgsqlParameter("@currentPage", model.PageNumber),
                new NpgsqlParameter("@pageSize", model.PageSize),
            };
            
            string sqlQuery = @"SELECT * FROM public.get_jobs(@companyId, @jobTitle, @ctcStart, @ctcEnd, @experience, @job_skills, @currentPage, @pageSize)";

            return await _context.GetJobsResponses.FromSqlRaw(sqlQuery.ToString(), parameter.ToArray()).ToListAsync();
        }

        public async Task<List<JobListModel>> GetPaginationAndFilterJob(int userId, JobListRequest model)
        {
            var jobSkillsArray = model.Skills != null ? model.Skills.ToArray() : (object)DBNull.Value;

            var parameters = new NpgsqlParameter[]
            {
                new("@page_number", NpgsqlDbType.Integer) { Value = model.PageNumber },
                new("@ctc_start", NpgsqlDbType.Integer) { Value = model.CtcStart },
                new("@ctc_end", NpgsqlDbType.Integer) { Value = model.CtcEnd },
                new("@user_id", NpgsqlDbType.Integer) { Value = userId },
                new("@experience", NpgsqlDbType.Double) {Value = model.Experience},
                new("@data_size", NpgsqlDbType.Integer) { Value = model.PageSize },
                new("@skills", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = jobSkillsArray },
                new("@job_name", NpgsqlDbType.Varchar) { Value = model.JobName ?? (object)DBNull.Value },
                new("@order_by", NpgsqlDbType.Varchar) { Value = model.OrderBy ?? (object)DBNull.Value },
                new("@direction", NpgsqlDbType.Varchar) { Value = model.Direction ?? (object)DBNull.Value}
            };

            string sql = "SELECT * FROM get_pagination_filtered_jobs(@page_number, @ctc_start, @ctc_end, @user_id, @experience, @data_size, @skills, @job_name, @order_by, @direction)";

            return await _context.GetJobList.FromSqlRaw(sql, parameters).ToListAsync();
        }

        public async Task<List<HighestPaidJobsResponse>> HighestPaidJobs()
        {
            return await _context.HighestPaidJobsResponses.FromSqlRaw("SELECT * FROM user_highest_paid_jobs()").ToListAsync();
        }

        public async Task<List<HighestPaidJobsResponse>> UserSkilllsJobs(int userId)
        {
            var parameters = new NpgsqlParameter[]
            {
                new("@userId", NpgsqlDbType.Integer) { Value = userId }
            };

            string sql = "SELECT * FROM user_skills_jobs(@userId)";

            return await _context.HighestPaidJobsResponses.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}





