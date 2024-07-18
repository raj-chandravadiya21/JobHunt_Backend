using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.JobApplication;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using JobHunt.Infrastructure.Interfaces;
using JobHunt.Domain.DataModels.Response;
using AutoMapper;

namespace JobHunt.Application.Services.UserService
{
    public class UserJobService(IHttpContextAccessor http, IUnitOfWork unitOfWork, IMapper mapper) : IUserJobService
    {
        private readonly IHttpContextAccessor _http = http;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public string GetUserId()
        {
            var token = GetTokenFromHeader.GetToken(_http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }

            return Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!)!;
        }

        public async Task<PaginatedResponse> FilterJobList(JobListRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.AspnetuserId.ToString() == GetUserId());

            List<JobListModel> jobList = await _unitOfWork.Job.GetPaginationAndFilterJob(user!.UserId, model);

            List<JobListResponse> data = _mapper.Map<List<JobListResponse>>(jobList);

            PaginatedResponse response = new()
            {
                ListOfData = data,
                CurrentPage = model.PageNumber,
                PageSize = model.PageSize
            };

            if (data.Count > 0)
            {
                response.TotalCount = (int)jobList[0].TotalCount;
            }

            return response;
        }
    }
}
