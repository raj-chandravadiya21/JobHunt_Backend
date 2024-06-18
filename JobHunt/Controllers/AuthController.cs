using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.Enum;
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
        public async Task<IActionResult> CheckUser([FromBody] CheckEmailDTO model)
        {
            return await _serviceBundle.AuthService.CheckUser(model);
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            return await _serviceBundle.AuthService.RegisterUser(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginAspNetUserDTO model)
        {
            return await _serviceBundle.AuthService.Login(model , (int)Role.User);
        }

        [HttpPost("check-company")]
        public async Task<IActionResult> CheckCompany([FromBody] CheckEmailDTO model)
        {
            return await _serviceBundle.AuthService.CheckCompany(model);
        }

        [HttpPost("register-company")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyDTO model)
        {
            return await _serviceBundle.AuthService.RegisterCompany(model);
        }

        [HttpPost("login-company")]
        public async Task<IActionResult> LoginCompany([FromBody] LoginAspNetUserDTO model)
        {
            return await _serviceBundle.AuthService.Login(model, (int)Role.Company);
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
