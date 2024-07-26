using JobHunt.Domain.DataModels.Response.Common;
using JobHunt.Domain.DataModels.Response.User.JobApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.CommonInterface
{
    public interface ICommonService
    {
        Task<JobDetails> GetJobDetails(int jobId);

        Task<List<SkillResponse>> GetAllSkill();

        Task<List<LanguageResponse>> GetAllLanguage();

        Task<JobHuntStatistics> Statistics();

        Task<ResumeResponse> GetResume(int applicationId);
    }
}
