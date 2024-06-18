using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers
{
    [Route("api/auth")]
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO model)
        {
            return await _serviceBundle.AuthService.LoginUser(model);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            return await _serviceBundle.AuthService.ForgotPasswordUser(model);
        }

        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetPasswordToken([FromBody] ValidateTokenDTO model)
        {
            return await _serviceBundle.AuthService.ValidateResetToken(model);
        }
    }
}
