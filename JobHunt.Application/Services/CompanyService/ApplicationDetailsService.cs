using AutoMapper;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.Enum;

namespace JobHunt.Application.Services.CompanyService
{
    public class ApplicationDetailsService(IHttpContextAccessor http, IUnitOfWork _unitOfWork, IMapper _mapper) : IApplicationDetailsService
    {
        public async Task<PaginatedResponse> GetApplicantDetail(JobSeekerDetailRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            var aspNetUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            List<JobSeekerDetailsResponse> JobSeekerResponse = await _unitOfWork.JobApplication.GetApplicantDetails(company.CompanyId, model);

            List<JobSeekerDetailsModel> data = _mapper.Map<List<JobSeekerDetailsModel>>(JobSeekerResponse);

            PaginatedResponse response = new()
            {
                ListOfData = data,
                CurrentPage = model.PageNumber,
                PageSize = model.PageSize
            };

            if (data.Count > 0)
            {
                response.TotalCount = JobSeekerResponse[0].TotalCount;
            }

            return response;
        }

        public async Task<JobSeekerCountWithStatus> GetJobSeekerCount(int jobId)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            var aspNetUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            JobSeekerCountWithStatus model = new()
            {
                AppliedCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.Applied && x.JobId == jobId),
                UnderReviewCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.UnderReview && x.JobId == jobId),
                ScheduleInterviewCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.ScheduleInterview && x.JobId == jobId),
                InterviewedCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.Interviewed && x.JobId == jobId),
                SelectedCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.Selected && x.JobId == jobId),
                RejectedCount = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.Rejected && x.JobId == jobId),
            };

            return model;
        }

        public async Task AcceptApplication(ApplicationStatusDetailRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            JobApplication? application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(x => x.Id == model.ApplicationId);

            application.StatusId = (int)ApplicationStatuses.UnderReview;
            application.ModifiedDate = DateTime.Now;

            _unitOfWork.JobApplication.UpdateAsync(application);
            await _unitOfWork.SaveAsync();

            ApplicationStatusLog log = new ApplicationStatusLog()
            {
                ApplicationId = model.ApplicationId,
                StatusId = (int)ApplicationStatuses.UnderReview,
                Notes = model.Notes,
                CreatedDate = DateTime.Now,
            };

             await _unitOfWork.ApplicationStatusLog.CreateAsync(log);
            await _unitOfWork.SaveAsync();
        }
    }
}
