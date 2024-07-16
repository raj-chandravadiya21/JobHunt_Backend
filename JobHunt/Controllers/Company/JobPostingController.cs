using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.Company
{
    [Route("api/company")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public JobPostingController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [CustomAuthorize("Company")]
        [HttpPost("create-job")]
        public async Task<IResult> CreateJob([FromBody] CreateJobRequest model)
        {
            await _serviceBundle.JobPostingService.CreateJob(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.AddSuccessfully, Messages.Job)));
        }

        [CustomAuthorize("Company User")]
        [HttpGet("view-job/{jobId}")]
        public async Task<IResult> ViewJobDetails(int jobId)
        {
            JobDetails data = await _serviceBundle.JobPostingService.GetJobDetails(jobId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [CustomAuthorize("Company")]
        [HttpGet("get-job-details/{jobId}")]
        public async Task<IResult> GetJobDetails(int jobId)
        {
            var data = await _serviceBundle.JobPostingService.GetEditJobDetails(jobId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [CustomAuthorize("Company")]    
        [HttpPost("get-jobs")]
        public async Task<IResult> GetJobs([FromBody] FilterJobRequest model)
        {
            var data = await _serviceBundle.JobPostingService.GetJobs(model);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}
