using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Response.Company.ApplicationDetails;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("jobSeeker-count-with-status")]
        public async Task<IResult> GetApplicantCount()
        {
            var data = await _serviceBundle.ApplicationDetailsService.GetJobSeekerCount();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}