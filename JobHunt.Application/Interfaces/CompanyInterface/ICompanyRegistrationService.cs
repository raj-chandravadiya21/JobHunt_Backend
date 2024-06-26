using JobHunt.Domain.DataModels.Request.CompanyRequest;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.CompanyInterface
{
    public interface ICompanyRegistrationService
    {
        Task CompanyProfile(CompanyRegistrationRequest model);

        Task<CompanyDetailsModel> GetCompanyDetails();
    }
}
