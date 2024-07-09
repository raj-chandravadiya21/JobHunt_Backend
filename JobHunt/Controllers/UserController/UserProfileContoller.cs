using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request.UserRequest.Profile;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    [CustomAuthorize("User")]
    public class UserProfileContoller(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle = serviceBundle;

        [HttpGet("get-user-profile")]
        public async Task<IResult> GetUserProfile()
        {
            var data = await _serviceBundle.UserProfileService.GetUserProfile();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-user-social-profile")]
        public async Task<IResult> GetUserSocialProfile()
        {
            var data = await _serviceBundle.UserProfileService.GetSocialProfile();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-user-education")]
        public async Task<IResult> GetUserEducation()
        {
            var data = await _serviceBundle.UserProfileService.GetUserEducation();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-user-project")]
        public async Task<IResult> GetUserProject()
        {
            var data = await _serviceBundle.UserProfileService.GetUserProject();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-user-experience")]
        public async Task<IResult> GetUserWorkExperience()
        {
            var data = await _serviceBundle.UserProfileService.GetUserWorkExperiences();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpPut("update-user-profile")]
        public async Task<IResult> UpdateUserProfile(UserProfileRequest model)
        {
            await _serviceBundle.UserProfileService.UpdateUserProfile(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), String.Format(Messages.UpdateSuccessfully, Messages.UserProfile)));
        }

        [HttpPut("update-work-experience")]
        public async Task<IResult> UpdateWorkExperience(UpdateWorkExperience model)
        {
            await _serviceBundle.UserProfileService.UpdateWorkExperience(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), String.Format(Messages.UpdateSuccessfully, Messages.WorkExperience)));
        }

        [HttpDelete("delete-work-experience/{id}")]
        public async Task<IResult> DeleteWorkExperience(int id)
        {
            await _serviceBundle.UserProfileService.DeleteWorkExperience(id);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),String.Format(Messages.DeleteSuccessfully, Messages.WorkExperience)));
        }

        [HttpPost("add-work-experience")]
        public async Task<IResult> AddWorkExperience(AddWorkExperienceRequest model)
        {
            await _serviceBundle.UserProfileService.AddWorkExperience(model);
            return Results.Ok(ResponseHelper.CreateResponse(new(),String.Format(Messages.AddSuccessfully, Messages.WorkExperience)));
        }

        [HttpPut("update-project")]
        public async Task<IResult> UpdateProject(UpdateProjectRequest model)
        {
            await _serviceBundle.UserProfileService.UpdateProject(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),String.Format(Messages.UpdateSuccessfully, Messages.Project)));
        }

        [HttpDelete("delete-project/{id}")]
        public async Task<IResult> DeleteProject(int id)
        {
            await _serviceBundle.UserProfileService.DeleteProject(id);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),String.Format(Messages.DeleteSuccessfully, Messages.Project)));
        }

        [HttpPost("add-project")]
        public async Task<IResult> AddProject(AddProjectRequest model)
        {
            await _serviceBundle.UserProfileService.AddProject(model);
            return Results.Ok(ResponseHelper.CreateResponse(new(),String.Format(Messages.AddSuccessfully, Messages.Project)));
        }

        [HttpPut("update-education")]
        public async Task<IResult> UpdateEducation(UpdateEducationRequest model)
        {
            await _serviceBundle.UserProfileService.UpdateEducation(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),String.Format(Messages.UpdateSuccessfully, Messages.Education)));
        }

        [HttpDelete("delete-education/{id}")]
        public async Task<IResult> DeleteEducation(int id)
        {
            await _serviceBundle.UserProfileService.DeleteEducation(id);
            return Results.Ok(ResponseHelper.SuccessResponse(new(), String.Format(Messages.DeleteSuccessfully, Messages.Education)));
        }

        [HttpPost("add-education")]
        public async Task<IResult> AddEducation(AddEducationRequest model)
        {
            await _serviceBundle.UserProfileService.AddEducation(model);
            return Results.Ok(ResponseHelper.CreateResponse(new(), String.Format(Messages.AddSuccessfully, Messages.Education)));
        }

        [HttpPost("update-social")]
        public async Task<IResult> UpdateSocialProfile(UserSocialProfileRequest model)
        {
            await _serviceBundle.UserProfileService.UpdateSocialProfile(model);
            return Results.Ok(ResponseHelper.SuccessResponse(new(),String.Format(Messages.UpdateSuccessfully, Messages.SocialProfile)));
        }
    }
}
