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

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspNetUserId);

            Company? company = await _unitOfWork.Company.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspNetUserId);

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
    }
}
