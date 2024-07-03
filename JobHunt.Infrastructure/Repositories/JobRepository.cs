using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
    }
}