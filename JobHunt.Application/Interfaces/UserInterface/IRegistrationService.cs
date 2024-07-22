using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.User.Registration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IRegistrationService
    {
        Task UserProfile(RegistrationUserRequest model);

        Task<List<DegreeTypeResponse>> GetAllDegreeType();

        Task<UserDetailsModel> GetUserDetails();
    }
}
