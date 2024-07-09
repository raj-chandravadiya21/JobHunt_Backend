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
        public async Task<IResult> ApplyJob(ApplyJobRequest model)
        {
            await _serviceBundle.ApplicationService.ApplyForJob(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), String.Format(Messages.JobApplied)));
        }
    }
}
