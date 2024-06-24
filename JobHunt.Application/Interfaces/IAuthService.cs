using JobHunt.Domain.DataModels.Request;

namespace JobHunt.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterUserRequest model);

        Task CheckUser(CheckEmailRequest model);

        Task<string> Login(LoginRequest model, int role);

        Task CheckCompany(CheckEmailRequest model);

        Task RegisterCompany(RegisterCompanyRequest model);

        Task ForgotPasswordUser(ForgotPasswordRequest model);

        Task ValidateResetToken(ValidateTokenRequest model);

        Task ResetPassword(ResetPasswordRequest model);
    }
}
