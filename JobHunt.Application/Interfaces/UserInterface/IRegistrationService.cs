using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IRegistrationService
    {
        Task<ResponseDTO> UserProfile(RegistrationUserDTO model);
    }
}
