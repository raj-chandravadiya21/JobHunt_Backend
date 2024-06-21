using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [ApiController]
    public class RegistrationUserController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public RegistrationUserController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("user-profile")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationUserDTO model)
        {
            return await _serviceBundle.RegistrationService.UserProfile(model);
        }

    }
}
