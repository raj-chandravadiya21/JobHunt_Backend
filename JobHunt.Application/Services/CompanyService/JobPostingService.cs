using AutoMapper;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
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

            Company? company = await _unitOfWork.Company.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

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
        }
    }
}
