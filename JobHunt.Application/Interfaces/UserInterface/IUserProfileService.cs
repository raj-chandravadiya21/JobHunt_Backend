using JobHunt.Domain.DataModels.Request.UserRequest.Profile;
using JobHunt.Domain.DataModels.Response.User;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IUserProfileService
    {
        Task<UserProfileModel> GetUserProfile();

        Task<List<UserSocialProfileResponse>> GetSocialProfile();

        Task<List<UserEducationResponse>> GetUserEducation();

        Task<List<UserProjectResponse>> GetUserProject();

        Task<List<UserWorkExperience>> GetUserWorkExperiences();

        Task UpdateUserProfile(UserProfileRequest model);

        Task UpdateWorkExperience(UpdateWorkExperience model);

        Task DeleteWorkExperience(int id);

        Task AddWorkExperience(AddWorkExperienceRequest model);

        Task UpdateProject(UpdateProjectRequest model);

        Task DeleteProject(int id);

        Task AddProject(AddProjectRequest model);

        Task UpdateEducation(UpdateEducationRequest model);

        Task DeleteEducation(int id);

        Task AddEducation(AddEducationRequest model);

        Task UpdateSocialProfile(UserSocialProfileRequest model);
    }
}
