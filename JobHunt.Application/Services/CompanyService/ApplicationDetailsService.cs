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
using JobHunt.Domain.Resource;

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

        public async Task SelectApplication(ApplicationStatusDetailRequest model)
        {
            JobApplication? application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(x => x.Id == model.ApplicationId);

            application.StatusId = (int)ApplicationStatuses.Selected;
            application.ModifiedDate = DateTime.Now;

            _unitOfWork.JobApplication.UpdateAsync(application);
            await _unitOfWork.SaveAsync();

            ApplicationStatusLog log = new ApplicationStatusLog();
            
            log.ApplicationId = model.ApplicationId;
            log.StatusId = (int)ApplicationStatuses.Selected;
            log.CreatedDate = DateTime.Now;
            log.Notes = model.Notes == null ? "-" : model.Notes;
            

            await _unitOfWork.ApplicationStatusLog.CreateAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task RejectApplication(ApplicationStatusDetailRequest model)
        {
            JobApplication? application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(x => x.Id == model.ApplicationId);

            application.StatusId = (int)ApplicationStatuses.Rejected;
            application.ModifiedDate = DateTime.Now;

            _unitOfWork.JobApplication.UpdateAsync(application);
            await _unitOfWork.SaveAsync();

            ApplicationStatusLog log = new ApplicationStatusLog();
            
            log.ApplicationId = model.ApplicationId;
            log.StatusId = (int)ApplicationStatuses.Rejected;
            log.CreatedDate = DateTime.Now;
            log.Notes = model.Notes == null ? "-" : model.Notes;

            await _unitOfWork.ApplicationStatusLog.CreateAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task InterviewedApplication(ApplicationStatusDetailRequest model)
        {
            JobApplication? application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(x => x.Id == model.ApplicationId);

            application.StatusId = (int)ApplicationStatuses.Interviewed;
            application.ModifiedDate = DateTime.Now;

            _unitOfWork.JobApplication.UpdateAsync(application);
            await _unitOfWork.SaveAsync();

            ApplicationStatusLog log = new ApplicationStatusLog();
                
            log.ApplicationId = model.ApplicationId;
            log.StatusId = (int)ApplicationStatuses.Interviewed;
            log.CreatedDate = DateTime.Now;
            log.Notes = model.Notes == null ? "-" : model.Notes;

            await _unitOfWork.ApplicationStatusLog.CreateAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task ScheduleInterview(InterviewDetailsRequest model)
        {
            if(model.InterviewDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new CustomException(Messages.EnterValidDate);
            }

            if(model.StartTime >= model.EndTime)
            {
                throw new CustomException(Messages.StartDateGreaterThenEndDate);
            }

            InterviewDetail interviewDetail = _mapper.Map<InterviewDetail>(model);

            interviewDetail.CreatedDate = DateTime.Now;

            ApplicationStatusLog log = new();

            log.ApplicationId = model.ApplicationId;
            log.StatusId = (int)ApplicationStatuses.ScheduleInterview;
            log.CreatedDate= DateTime.Now;
            log.Notes = model.Notes == null ? "-" : model.Notes;

            JobApplication? application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(x => x.Id == model.ApplicationId);

            application.StatusId = (int)ApplicationStatuses.ScheduleInterview;
            application.ModifiedDate = DateTime.Now;

            _unitOfWork.JobApplication.UpdateAsync(application);

            await _unitOfWork.InterviewDetail.CreateAsync(interviewDetail);
            await _unitOfWork.ApplicationStatusLog.CreateAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task<InterviewDetailsReponse> GetInterviewDetails(int applicationId)
        {
            InterviewDetail? interviewDetail = await _unitOfWork.InterviewDetail.GetFirstOrDefaultAsync(x => x.ApplicationId == applicationId);
            ApplicationStatusLog? log = await _unitOfWork.ApplicationStatusLog.GetFirstOrDefault(x => x.ApplicationId == applicationId && x.StatusId == (int)ApplicationStatuses.ScheduleInterview);

            InterviewDetailsReponse interviewDetailsReponse = new()
            {
                EndTime = interviewDetail.EndTime,
                StartTime = interviewDetail.StartTime,
                Location = interviewDetail.Location,
                InterviewDate = interviewDetail.InterviewDate,
                Notes = log.Notes
            };

            return interviewDetailsReponse;
        }

        public async Task UpdateInterviewDetails(InterviewDetailsRequest model)
        {
            InterviewDetail? details = await _unitOfWork.InterviewDetail.GetFirstOrDefaultAsync(x => x.ApplicationId == model.ApplicationId);

            details.InterviewDate = model.InterviewDate;
            details.Location = model.Location;
            details.StartTime= model.StartTime;
            details.EndTime = model.EndTime;
            details.ModifiedDate = DateTime.Now;

            _unitOfWork.InterviewDetail.UpdateAsync(details);
            await _unitOfWork.SaveAsync();

            ApplicationStatusLog? log = await _unitOfWork.ApplicationStatusLog.GetFirstOrDefault(x => x.ApplicationId == model.ApplicationId && x.StatusId == (int)ApplicationStatuses.ScheduleInterview);

            log.Notes = model.Notes;

            _unitOfWork.ApplicationStatusLog.UpdateAsync(log);
            await _unitOfWork.SaveAsync();
        }
    }
}
