using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
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

        #region check-user-sendOTP
        [HttpPost("check-user")]
        public async Task<IResult> CheckUser([FromBody] CheckEmailRequest model)
        {
             await _serviceBundle.AuthService.CheckEmail(model, (int)Role.User);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.OtpSent)));
        }
        #endregion  

        #region register-user
        [HttpPost("register-user")]
        public async Task<IResult> RegisterUser([FromBody] RegisterUserRequest model)
        {
             await _serviceBundle.AuthService.RegisterUser(model);
            return Results.Ok(ResponseHelper.CreateResponse(new(), string.Format(Messages.CreatedSuccessfully, Messages.User)));

        }
        #endregion

        #region LoginUser
        [HttpPost("login")]
        public async Task<IResult> LoginUser([FromBody] LoginRequest model)
        {
            var data =  await _serviceBundle.AuthService.Login(model , (int)Role.User);
            return Results.Ok(ResponseHelper.SuccessResponse(data, string.Format(Messages.LoginSuccessfully, Messages.User)));
        }
        #endregion

        #region Check-company-sendOTP
        [HttpPost("check-company")]
        public async Task<IResult> CheckCompany([FromBody] CheckEmailRequest model)
        {
             await _serviceBundle.AuthService.CheckEmail(model, (int)Role.Company);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.OtpSent)));
        }
        #endregion

        #region register-company
        [HttpPost("register-company")]
        public async Task<IResult> RegisterCompany([FromBody] RegisterCompanyRequest model)
        {
             await _serviceBundle.AuthService.RegisterCompany(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.CreatedSuccessfully, Messages.Company)));
        }
        #endregion

        #region Login-Company
        [HttpPost("login-company")]
        public async Task<IResult> LoginCompany([FromBody] LoginRequest model)
        {
            var data = await _serviceBundle.AuthService.Login(model, (int)Role.Company);
            return Results.Ok(ResponseHelper.SuccessResponse(data, string.Format(Messages.LoginSuccessfully, Messages.Company)));
        }
        #endregion

        #region Forgot-password
        [HttpPost("forgot-password")]
        public async Task<IResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
             await _serviceBundle.AuthService.ForgotPasswordUser(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.SentSuccessfully, Messages.ResetPasswordLink)));
        }
        #endregion

        #region validate-resetPassword-token
        [HttpPost("validate-reset-token")]
        public async Task<IResult> ValidateResetPasswordToken([FromBody] ValidateTokenRequest model)
        {
            await _serviceBundle.AuthService.ValidateResetToken(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new()));
        }
        #endregion

        #region reset-password
        [HttpPost("reset-password")]
        public async Task<IResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
             await _serviceBundle.AuthService.ResetPassword(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.UpdateSuccessfully, Messages.Password)));
        }
        #endregion
    }
}
