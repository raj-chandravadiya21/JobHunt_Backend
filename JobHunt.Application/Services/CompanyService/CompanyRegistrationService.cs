using AutoMapper;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Domain.DataModels.Request.CompanyRequest;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace JobHunt.Application.Services.CompanyService
{
    public class CompanyRegistrationService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICompanyRegistrationService
    {
        public async Task CompanyProfile(CompanyRegistrationRequest model, string token)
        {
            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Company? company = await _unitOfWork.Company.GetFirstOrDefault(x => x.AspnetuserId.ToString() == aspnetId);

            company.MobileNumber = model.PhoneNumber;
            company.Website = model.WebsiteUrl;
            company.EstablisedDate = model.EstablishedDate;
            company.Address = model.Address;
            company.Logo = model.Logo;
            company.Description = model.Description;
            company.ModifiedDate = DateTime.Now;

            _unitOfWork.Company.UpdateAsync(company);
            await _unitOfWork.SaveAsync();
        }
    }
}
