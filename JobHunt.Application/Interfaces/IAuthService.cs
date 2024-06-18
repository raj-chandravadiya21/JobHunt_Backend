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

        Task<ResponseDTO> CheckUser(CheckUserDTO model);
    }
}
