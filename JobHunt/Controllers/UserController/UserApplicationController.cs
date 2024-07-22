using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    [CustomAuthorize("User")]
    public class UserApplicationController(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle = serviceBundle;

        [HttpPost("apply-for-job")]
        public async Task<IResult> ApplyJob([FromForm]ApplyJobRequest model)
        {
            await _serviceBundle.ApplicationService.ApplyForJob(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), String.Format(Messages.JobApplied)));
        }

        [HttpPost("user-applications")]
        public async Task<IResult> UserApplication([FromBody]UserApplicationsRequest model)
        {
           var data =  await _serviceBundle.ApplicationService.UserApplication(model);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("user-job-application/{jobApplicationId}")]
        public async Task<IResult> UserJobApplication(int jobApplicationId)
        {
            var data = await _serviceBundle.ApplicationService.JobApplicationStatus(jobApplicationId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("download-resume/{applicationId}")]
        public async Task<IActionResult> DownloadResume(int applicationId)
        {
            var data = await _serviceBundle.ApplicationService.GetResume(applicationId);

            return File(data.FileBytes!, data.ContentType!, data.FileName);
        }
    }
}
