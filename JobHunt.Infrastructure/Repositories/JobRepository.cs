﻿using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
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
                new NpgsqlParameter("@job_skills", NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = jobSkillsArray }
            };

            string sqlQuery = @"SELECT * FROM public.get_jobs(
                @companyId, 
                @jobTitle, 
                @ctcStart, 
                @ctcEnd, 
                @experience, 
                @job_skills
            )";

            var data = await _context.GetJobsResponses.FromSqlRaw(sqlQuery.ToString(), parameter.ToArray()).ToListAsync();

            return data;
        }
    }
}