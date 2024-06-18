using JobHunt.Application.Services;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO> RegisterUser(RegisterUserDTO model);

        Task<ResponseDTO> CheckUser(CheckEmailDTO model);

        Task<ResponseDTO> Login(LoginAspNetUserDTO model, int role);

        Task<ResponseDTO> CheckCompany(CheckEmailDTO model);

        Task<ResponseDTO> RegisterCompany(RegisterCompanyDTO model);

        Task<ResponseDTO> ForgotPasswordUser(ForgotPasswordDTO model);

        Task<ResponseDTO> ValidateResetToken(ValidateTokenDTO model);
    }
}
