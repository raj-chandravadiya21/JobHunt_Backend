using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.Company
{
    [Route("api/company")]
    [ApiController]
    [CustomAuthorize("Company")]
    public class CompanyRegistrationController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public CompanyRegistrationController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("company-profile")]
        public async Task<IResult> RegisterCompany([FromBody] CompanyRegistrationRequest model)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            await _serviceBundle.CompanyRegistrationService.CompanyProfile(model, token);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.CompleteSuccessfully, Messages.Register)));
        }

        //[HttpGet("get-user-details")]
        //public async Task<IResult> GetUserDetails()
        //{
        //    string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

        //    var data = await _serviceBundle.RegistrationService.GetUserDetails(token);
        //    return Results.Ok(ResponseHelper.SuccessResponse(data));
        //}
    }
}
