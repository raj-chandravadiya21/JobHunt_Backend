using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.JobPosting;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;

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
    }
}