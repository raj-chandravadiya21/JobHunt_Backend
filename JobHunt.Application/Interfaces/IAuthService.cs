using JobHunt.Domain.DataModels.Request;

namespace JobHunt.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterUserRequest model);

        Task CheckEmail(CheckEmailRequest model, int role);

        Task<string> Login(LoginRequest model, int role);

        Task RegisterCompany(RegisterCompanyRequest model);

        Task ForgotPasswordUser(ForgotPasswordRequest model);

        Task ValidateResetToken(ValidateTokenRequest model);

        Task ResetPassword(ResetPasswordRequest model);
    }
}
