﻿using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.CompanyRequest.Registration;
using JobHunt.Domain.DataModels.Request.UserRequest.Registration;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.Company
{
    [Route("api/company")]
    [ApiController]
    [CustomAuthorize("Company")]
    public class CompanyRegistrationController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public CompanyRegistrationController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpPut("company-registration")]
        public async Task<IResult> RegisterCompany([FromBody] CompanyRegistrationRequest model)
        {
            await _serviceBundle.CompanyRegistrationService.CompanyProfile(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), string.Format(Messages.UpdateSuccessfully, Messages.CompanyDetails)));
        }

        [HttpGet("get-company")]
        public async Task<IResult> GetCompany()
        {
            var data = await _serviceBundle.CompanyRegistrationService.GetCompany();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-company-details")]
        public async Task<IResult> GetCompanyDetails()
        {
            var data = await _serviceBundle.CompanyRegistrationService.GetCompanyDetails();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}
