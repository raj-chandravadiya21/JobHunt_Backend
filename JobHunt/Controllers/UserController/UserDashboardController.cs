using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using JobHunt.Domain.DataModels.Response.User.Dashboard;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [Route("api/user-dashboard")]
    [ApiController]
    [CustomAuthorize("User")]
    public class UserDashboardController(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle = serviceBundle;

        [HttpGet("highest-paid-job")]
        public async Task<IResult> HighestPaidJob()
        {
            var jobs = await _serviceBundle.UserDashboardService.HighestPaidJobs();
            return Results.Ok(ResponseHelper.SuccessResponse(jobs));
        }

        [HttpGet("user-skills-job")]
        public async Task<IResult> UserSkilllsJob()
        {
            var jobs = await _serviceBundle.UserDashboardService.UserSkilllsJobs();
            return Results.Ok(ResponseHelper.SuccessResponse(jobs));
        }
    }
}
