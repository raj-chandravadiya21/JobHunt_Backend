using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    public class RegistrationUserController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public RegistrationUserController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("user-profile")]
        public async Task<IResult> RegisterUser([FromBody] RegistrationUserRequest model)
        {
             await _serviceBundle.RegistrationService.UserProfile(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),string.Format(Messages.CompleteSuccessfully,Messages.Register)));
        }

        [HttpGet("get-skill")]
        public async Task<IResult> GetAllSkills()
        {
            var data = await _serviceBundle.RegistrationService.GetAllSkill();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-language")]
        public async Task<IResult> GetAllLanguage()
        {
            var data = await _serviceBundle.RegistrationService.GetAllLanguage();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-degree-type")]
        public async Task<IResult> GetAllDegreeType()
        {
            var data = await _serviceBundle.RegistrationService.GetAllDegreeType();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}
