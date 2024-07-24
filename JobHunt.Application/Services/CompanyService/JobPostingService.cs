using AutoMapper;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Services.CompanyService
{
    public class JobPostingService(IUnitOfWork _unitOfWork, IHttpContextAccessor http, IMapper _mapper) : IJobPostingService
    {
        public async Task CreateJob(CreateJobRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspnetId);

            Job job = new()
            {
                CompanyId = company.CompanyId,
                JobName = model.Name,
                Location = model.Location,
                StartDate = model.JobStartDate,
                CtcStart = model.CTCStart,
                CtcEnd = model.CTCEnd,
                ExperienceInYears = (int)model.Experience,
                LastDateToApply = model.LastDate,
                NoOfOpenings = model.NoOfOpening,
                JobDescription = model.Description,
                Requirements = model.Requirement,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Job.CreateAsync(job);
            await _unitOfWork.SaveAsync();

            for (int i = 0; i < model.Skills.Count; i++)
            {
                JobSkill jobSkill = new()
                {
                    JobId = job.Id,
                    SkillId = model.Skills[i]
                };
                await _unitOfWork.JobSkill.CreateAsync(jobSkill);
            }

            for (int i = 0; i < model.Responsibility.Count; i++)
            {
                JobResponsibility jobResponsibility = new()
                {
                    JobId = job.Id,
                    Responsibility = model.Responsibility[i]
                };
                await _unitOfWork.JobResponsibility.CreateAsync(jobResponsibility);
            }

            for (int i = 0; i < model.Perks.Count; i++)
            {
                JobPerk jobPerks = new()
                {
                    JobId = job.Id,
                    Perks = model.Perks[i]
                };
                await _unitOfWork.JobPerks.CreateAsync(jobPerks);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<EditJobDetailsResponse> GetEditJobDetails(int jobId)
            => await _unitOfWork.Job.GetJobDetails(jobId);

        public async Task UpdateJobDetails(UpdateJobRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspnetId);

            List<JobSkill> oldSkillsList = await _unitOfWork.JobSkill.WhereList(x => x.JobId == model.JobId);

            List<JobResponsibility> JobResponsibilities = await _unitOfWork.JobResponsibility.WhereList(x=>x.JobId == model.JobId);

            List<JobPerk> JobPerks = await _unitOfWork.JobPerks.WhereList(x => x.JobId == model.JobId);

            for (int i = 0; i < oldSkillsList.Count; i++)
            {                
                _unitOfWork.JobSkill.Remove(oldSkillsList[i]);
            }

            for (int i = 0; i < JobResponsibilities.Count; i++)
            {
                _unitOfWork.JobResponsibility.Remove(JobResponsibilities[i]);
            }

            for (int i = 0; i < JobPerks.Count; i++)
            {
                _unitOfWork.JobPerks.Remove(JobPerks[i]);
            }
            await _unitOfWork.SaveAsync();

            Job? job = await _unitOfWork.Job.GetFirstOrDefault(x => x.Id == model.JobId);

            job.CompanyId = company.CompanyId;
            job.JobName = model.Name;
            job.Location = model.Location;
            job.StartDate = model.JobStartDate;
            job.CtcStart = model.CTCStart;
            job.CtcEnd = model.CTCEnd;
            job.ExperienceInYears = (int)model.Experience;
            job.LastDateToApply = model.LastDate;
            job.NoOfOpenings = model.NoOfOpening;
            job.JobDescription = model.Description;
            job.Requirements = model.Requirement;
            job.CreatedDate = DateTime.Now;

            _unitOfWork.Job.UpdateAsync(job);
            await _unitOfWork.SaveAsync();

            for (int i = 0; i < model.Skills.Count; i++)
            {
                JobSkill jobSkill = new()
                {
                    JobId = job.Id,
                    SkillId = model.Skills[i]
                };
                await _unitOfWork.JobSkill.CreateAsync(jobSkill);
            }

            for (int i = 0; i < model.Responsibility.Count; i++)
            {
                JobResponsibility jobResponsibility = new()
                {
                    JobId = job.Id,
                    Responsibility = model.Responsibility[i]
                };
                await _unitOfWork.JobResponsibility.CreateAsync(jobResponsibility);
            }

            for (int i = 0; i < model.Perks.Count; i++)
            {
                JobPerk jobPerks = new()
                {
                    JobId = job.Id,
                    Perks = model.Perks[i]
                };
                await _unitOfWork.JobPerks.CreateAsync(jobPerks);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedResponse> GetJobs(FilterJobRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspnetId);

            List<GetJobsResponse> jobReponse = await _unitOfWork.Job.GetJobs(company.CompanyId, model);

            List<GetJobListResponce> data = _mapper.Map<List<GetJobListResponce>>(jobReponse);

            PaginatedResponse response = new()
            {
                ListOfData = data,
                CurrentPage = model.PageNumber,
                PageSize = model.PageSize
            };

            if (data.Count > 0)
            {
                response.TotalCount = jobReponse[0].TotalCount;
            }

            return response;
        }
    }
}
