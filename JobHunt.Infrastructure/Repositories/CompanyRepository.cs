using JobHunt.Domain.DataModels.Response.Common;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Infrastructure.Repositories
{
    public class CompanyRepository(DefaultdbContext context) : Repository<Company>(context), ICompanyRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task<JobDetails> JobDetails(int jobId)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@job_id",jobId),
            };

            return await _context.JobDetails.FromSqlRaw("SELECT * FROM job_information(@job_id)", parameter).FirstAsync();
        }
    }
}
