﻿using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response.Company;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface IJobPostingService
    {
        Task CreateJob(CreateJobRequest model);

        Task<JobDetails> GetJobDetails(int jobId);

        Task<EditJobDetailsResponse> GetEditJobDetails(int jobId);

        Task<List<GetJobsResponse>> GetJobs();
    }
}
