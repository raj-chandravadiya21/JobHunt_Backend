using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobHunt.Controllers.Company
{
    [Route("api/company")]
    [ApiController]
    [CustomAuthorize("Company")]
    public class JobPostingController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public JobPostingController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("create-job")]
        public async Task<IResult> CreateJob([FromBody] CreateJobRequest model)
        {
            await _serviceBundle.JobPostingService.CreateJob(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.AddSuccessfully, Messages.Job)));
        }

        [HttpGet("view-job/{jobId}")]
        public async Task<IResult> ViewJobDetails(int jobId)
        {
            JobDetails data = await _serviceBundle.JobPostingService.GetJobDetails(jobId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }


    }
}
