using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.UserRequest.JobApplication;
using JobHunt.Domain.Helper;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    [CustomAuthorize("User")]
    public class UserJobController(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _sereviceBundle = serviceBundle;

        [HttpPost("get-jobs")]
        public async Task<IResult> GetJobsForUser([FromBody]JobListRequest model)
        {
           var data = await _sereviceBundle.UserJobService.FilterJobList(model);
           return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}
