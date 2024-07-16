using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobHunt.Infrastructure.Repositories
{
    public class UserEducationRepository(DefaultdbContext context) : Repository<UserEducation>(context), IUserEducationRepository 
    {
        private readonly DefaultdbContext _context = context;

        public async Task<List<UserEducationModel>> GetUserEducationInformation(int userId)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@userId", userId)
            };

            return await _context.UserEducationModel.FromSqlRaw("SELECT * FROM public.get_user_education_details(@userId)", parameter).ToListAsync();
        }
    }
}
