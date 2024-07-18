﻿using AutoMapper;
using JobHunt.Application.Interfaces.CompanyInterface;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.DataModels.Response.Company;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Transactions;

namespace JobHunt.Application.Services.CompanyService
{
    public class CompanyRegistrationService(IUnitOfWork _unitOfWork, IMapper _mapper, IHttpContextAccessor http) : ICompanyRegistrationService
    {
        public async Task CompanyProfile(CompanyRegistrationRequest model)
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("User is not valid");
            }

            var aspnetId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspnetId);

            company.MobileNumber = model.PhoneNumber;
            company.Website = model.WebsiteUrl;
            company.EstablisedDate = model.EstablishedDate;
            company.Address = model.Address;
            company.Logo = model.Logo;
            company.Description = model.Description;
            company.ModifiedDate = DateTime.Now;

            _unitOfWork.Company.UpdateAsync(company);
            await _unitOfWork.SaveAsync();

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspnetId);

            aspnetuser.IsRegistered = true;

            _unitOfWork.AspNetUser.UpdateAsync(aspnetuser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CompanyDetailsModel> GetCompany()
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            var aspNetUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId == aspnetuser!.AspnetuserId);

            CompanyDetailsModel model = new();

            if (aspnetuser.IsRegistered)
            {
                model.Name = company.CompanyName;
                model.Email = company.Email;
                model.IsRegistred = true;
            }
            else
            {
                model.IsRegistred = false;
            }

            return model;
        }

        public async Task<CompanyDetailsResponse> GetCompanyDetails()
        {
            var token = GetTokenFromHeader.GetToken((HttpContextAccessor)http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException("Session is Expired.");
            }

            var aspNetUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            Aspnetuser? aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefaultAsync(x => x.AspnetuserId.ToString() == aspNetUserId);

            Company? company = await _unitOfWork.Company.GetFirstOrDefaultAsync(x => x.AspnetuserId == aspnetuser!.AspnetuserId);

            CompanyDetailsResponse model = new()
            {
                ComapanyName = company.CompanyName,
                Website = company.Website,
                EstablishedDate = company.EstablisedDate,
                Address = company.Address,
                Email = company.Email,
                Logo = company.Logo,
                Phone = company.MobileNumber,
                Description = company.Description
            };

            return model;
        }
    }
}
