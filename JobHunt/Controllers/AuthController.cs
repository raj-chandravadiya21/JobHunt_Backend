using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public AuthController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPost("check-user")]
        public async Task<IActionResult> CheckUser([FromBody] CheckUserDTO model)
        {
            return await _serviceBundle.AuthService.CheckUser(model);
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            return await _serviceBundle.AuthService.RegisterUser(model);
        }
    }
}
