using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobHunt.Controllers.Company
{
    [Route("api/company")]
    [ApiController]
    [CustomAuthorize("Company")]
    public class ApplicationDetailsController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public ApplicationDetailsController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("jobSeeker-details")]
        public async Task<IResult> GetApplicantDetails([FromBody] JobSeekerDetailRequest model)
        {
            var data = await _serviceBundle.ApplicationDetailsService.GetApplicantDetail(model);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("jobSeeker-count-with-status/{jobId}")]
        public async Task<IResult> GetApplicantCount(int jobId)
        {
            var data = await _serviceBundle.ApplicationDetailsService.GetJobSeekerCount(jobId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpPost("accept-application")]
        public async Task<IResult> AcceptApplication([FromBody] ApplicationStatusDetailRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.AcceptApplication(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.Accepted, Messages.Application)));
        }

        [HttpPost("select-application")]
        public async Task<IResult> SelectApplication([FromBody] ApplicationStatusDetailRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.SelectApplication(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.Selected, Messages.Candidate)));
        }

        [HttpPost("reject-application")]
        public async Task<IResult> RejectApplication([FromBody] ApplicationStatusDetailRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.RejectApplication(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.Rejected, Messages.Candidate)));
        }

        [HttpPost("interviewed-application")]
        public async Task<IResult> InterviewedApplication([FromBody] ApplicationStatusDetailRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.InterviewedApplication(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.CompleteSuccessfully, Messages.Interview)));
        }

        [HttpPost("schedule-interview")]
        public async Task<IResult> ScheduleInterview([FromBody] InterviewDetailsRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.ScheduleInterview(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new()));
        }

        [HttpGet("get-interview-details/{applicationId}")]
        public async Task<IResult> GetInterviewDetails(int applicationId)
        {
            var data = await _serviceBundle.ApplicationDetailsService.GetInterviewDetails(applicationId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpPut("update-interview-details")]
        public async Task<IResult> UpdateInterviewDetails([FromBody] InterviewDetailsRequest model)
        {
            await _serviceBundle.ApplicationDetailsService.UpdateInterviewDetails(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new()));
        }
    }
}