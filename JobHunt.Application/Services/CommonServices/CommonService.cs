﻿using AutoMapper;
using JobHunt.Application.Interfaces.CommonInterface;
using JobHunt.Domain.DataModels.Response.Common;
using JobHunt.Domain.DataModels.Response.User.Dashboard;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JobHunt.Domain.Enum;
using JobHunt.Domain.DataModels.Response.User.JobApplication;

namespace JobHunt.Application.Services.CommonServices
{
    public class CommonService(IUnitOfWork unitOfWork, IMapper mapper): ICommonService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<JobDetails> GetJobDetails(int jobId)
            => await _unitOfWork.Company.JobDetails(jobId);

        public async Task<List<SkillResponse>> GetAllSkill()
        {
            return _mapper.Map<List<SkillResponse>>(await _unitOfWork.Skill.GetAllAsync());
        }

        public async Task<List<LanguageResponse>> GetAllLanguage()
        {
            return _mapper.Map<List<LanguageResponse>>(await _unitOfWork.Language.GetAllAsync());
        }

        public async Task<JobHuntStatistics> Statistics()
        {
            JobHuntStatistics model = new JobHuntStatistics();

            model.TotalCompany = await _unitOfWork.Company.CountAsync();
            model.TotalJobs = await _unitOfWork.Job.CountAsync();
            model.PlacedUsers = await _unitOfWork.JobApplication.ConditionalCount(x => x.StatusId == (int)ApplicationStatuses.Selected);

            return model;
        }

        public async Task<ResumeResponse> GetResume(int applicationId)
        {
            var application = await _unitOfWork.JobApplication.GetFirstOrDefaultAsync(u => u.Id == applicationId);

            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            var filePath = Path.Combine(parentDirectory!, "JobHunt.Domain", "Resumes", application!.Resume!);

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);

            return new ResumeResponse
            {
                FileBytes = fileBytes,
                ContentType = "application/octet-stream",
                FileName = fileName,
            };
        }
    }
}
