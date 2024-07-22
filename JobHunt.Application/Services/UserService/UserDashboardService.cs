using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Infrastructure.Interfaces;
using JobHunt.Domain.DataModels.Response.User.Dashboard;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobHunt.Application.Services.UserService
{
    public class UserDashboardService(IUnitOfWork _unitOfWork, IHttpContextAccessor http) : IUserDashboardService
    {
        public async Task<List<HighestPaidJobsResponse>> HighestPaidJobs()
        {
            return await _unitOfWork.Job.HighestPaidJobs();
        }

        public async Task<List<HighestPaidJobsResponse>> UserSkilllsJobs()
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            User? user = await _unitOfWork.User.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);
            var userId = user.UserId;

            return await _unitOfWork.Job.UserSkilllsJobs(userId);
        }
    }
}
