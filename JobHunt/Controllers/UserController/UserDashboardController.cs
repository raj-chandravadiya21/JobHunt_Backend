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
    public class UserDashboardController(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle = serviceBundle;

        [HttpGet("highest-paid-job")]
        public async Task<IResult> HighestPaidJobs()
        {
            var jobs = await _serviceBundle.UserDashboardService.HighestPaidJobs();
            return Results.Ok(ResponseHelper.SuccessResponse(jobs));
        }
    }
}
