using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace JobHunt.Application.Services.UserService
{
    public class ApplicationService(IUnitOfWork unitOfWork, IHttpContextAccessor http, IMapper mapper) : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _http = http;
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


        public async Task ApplyForJob(ApplyJobRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var current_timestamp = DateTime.Now;

            JobApplication jobApplication = new()
            {
                JobId = model.JobId,
                UserId = user!.UserId,
                Description = model.Description,
                AppliedDate = current_timestamp,
                StatusId = (int)ApplicationStatuses.Applied,
            };

            await _unitOfWork.JobApplication.CreateAsync(jobApplication);
            await _unitOfWork.SaveAsync();

            if (model.IsUploadFromProfile && model.Resume != null && model.Resume.Length > 0)
            {
                string newFileName = $"{jobApplication.Id}_{current_timestamp:ddMMyy_hhmmss}_" + model.Resume.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Domain/Resumes", newFileName);
                using var stream = System.IO.File.Create(filePath);
                await model.Resume.CopyToAsync(stream);
            }
        }

        
    }
}
