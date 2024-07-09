using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace JobHunt.Application.Services.UserService
{
    public class ApplicationService(IUnitOfWork _unitOfWork, IHttpContextAccessor http, IMapper _mapper) : IApplicationService
    {
        public async Task ApplyForJob(ApplyJobRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

            User? user = await _unitOfWork.User.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

            JobApplication jobApplication = _mapper.Map<JobApplication>(model);

            jobApplication.UserId = user.UserId;
            jobApplication.StatusId = (int)ApplicationStatuses.Applied;
            jobApplication.AppliedDate = DateTime.Now;

            await _unitOfWork.JobApplication.CreateAsync(jobApplication);
            await _unitOfWork.SaveAsync();
        }
    }
}
