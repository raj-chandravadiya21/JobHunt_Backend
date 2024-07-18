﻿using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface IApplicationDetailsService
    {
        Task<PaginatedResponse> GetApplicantDetail(JobSeekerDetailRequest model);

        Task<JobSeekerCountWithStatus> GetJobSeekerCount(int jobId);

        Task AcceptApplication(ApplicationStatusDetailRequest model);

        Task SelectApplication(ApplicationStatusDetailRequest model);

        Task RejectApplication(ApplicationStatusDetailRequest model);

        Task InterviewedApplication(ApplicationStatusDetailRequest model);

        Task ScheduleInterview(InterviewDetailsRequest model);

        Task<InterviewDetailsReponse> GetInterviewDetails(int applicationId);

        Task UpdateInterviewDetails(InterviewDetailsRequest model);

    }
}
