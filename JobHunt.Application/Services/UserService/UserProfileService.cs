using AutoMapper;
using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.DataModels.Request.UserRequest.Profile;
using JobHunt.Domain.DataModels.Response.User;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobHunt.Application.Services.UserService
{
    public class UserProfileService(IUnitOfWork unitOfWork, IHttpContextAccessor http, IMapper mapper) : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _http = http;
        private readonly IMapper _mapper = mapper;

        public string GetUserId()
        {
            var token = GetTokenFromHeader.GetToken(_http);

            var isValidToken = Jwt.ValidateToken(token, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }
            
            return Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!)!;
        }

        public async Task<UserProfileModel> GetUserProfile()
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            return await _unitOfWork.User.GetUserProfile(user!.UserId);
        }

        public async Task<List<UserSocialProfileResponse>> GetSocialProfile()
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            return _mapper.Map<List<UserSocialProfileResponse>>(await _unitOfWork.UserSocialProfile.WhereList(u => u.UserId == user!.UserId));
        }

        public async Task<List<UserEducationResponse>> GetUserEducation()
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            return _mapper.Map<List<UserEducationResponse>>(await _unitOfWork.UserEducation.WhereList(u => u.UserId == user!.UserId));
        }

        public async Task<List<UserProjectResponse>> GetUserProject()
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            return _mapper.Map<List<UserProjectResponse>>(await _unitOfWork.Project.WhereList(u => u.UserId == user!.UserId));
        }

        public async Task<List<UserWorkExperience>> GetUserWorkExperiences()
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            return _mapper.Map<List<UserWorkExperience>>(await _unitOfWork.WorkExperiment.WhereList(u => u.UserId == user!.UserId));
        }

        public async Task UpdateUserProfile(UserProfileRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());   
            
            _mapper.Map<UserProfileRequest, User>(model, user!);

            user!.ModifiedDate = DateTime.Now;

            _unitOfWork.User.UpdateAsync(user!);

            _unitOfWork.UserSkill.RemoveRange(await _unitOfWork.UserSkill.WhereList(u => u.UserId == user.UserId));
            _unitOfWork.UserLanguage.RemoveRange(await _unitOfWork.UserLanguage.WhereList(u => u.UserId == user.UserId));

            for (int i = 0; i < model.Skills.Count; i++)
            {
                UserSkill userSkill = new()
                {
                    UserId = user.UserId,
                    SkillId = model.Skills[i]
                };
                await _unitOfWork.UserSkill.CreateAsync(userSkill);
            }

            for (int i = 0; i < model.Languages.Count; i++)
            {
                UserLanguage userLanguage = new()
                {
                    UserId = user.UserId,
                    LanguageId = model.Languages[i]
                };
                await _unitOfWork.UserLanguage.CreateAsync(userLanguage);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateWorkExperience(UpdateWorkExperience model)
        {
            if (model.StartDate >= model.EndDate)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var workExperience = await _unitOfWork.WorkExperiment.GetFirstOrDefault(u => u.Id == model.Id && u.UserId == user!.UserId);

            _mapper.Map<UpdateWorkExperience, WorkExperience>(model, workExperience!);

            _unitOfWork.WorkExperiment.UpdateAsync(workExperience!);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteWorkExperience(int id)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var workExperience = await _unitOfWork.WorkExperiment.GetFirstOrDefault(u => u.Id == id && u.UserId == user!.UserId);

            _unitOfWork.WorkExperiment.Remove(workExperience!);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddWorkExperience(AddWorkExperienceRequest model)
        {
            if (model.StartDate >= model.EndDate)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            WorkExperience workExperience = new();

            _mapper.Map<AddWorkExperienceRequest, WorkExperience>(model, workExperience);

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            workExperience.UserId = user!.UserId;

            workExperience.CreatedDate = DateTime.Now;

            await _unitOfWork.WorkExperiment.CreateAsync(workExperience);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProject(UpdateProjectRequest model)
        {
            if(model.StartDate >= model.EndDate)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var project = await _unitOfWork.Project.GetFirstOrDefault(u => u.Id == model.Id && u.UserId == user!.UserId);

            _mapper.Map<UpdateProjectRequest, Project>(model, project!);

             _unitOfWork.Project.UpdateAsync(project!);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProject(int id)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var project = await _unitOfWork.Project.GetFirstOrDefault(u => u.Id == id && u.UserId == user!.UserId);

            _unitOfWork.Project.Remove(project!);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddProject(AddProjectRequest model)
        {
            if (model.StartDate >= model.EndDate)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            Project project = new();

            _mapper.Map<AddProjectRequest, Project>(model, project);

            project.UserId = user!.UserId;
            project.CreatedDate = DateTime.Now;

            await _unitOfWork.Project.CreateAsync(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEducation(UpdateEducationRequest model)
        {
            if (model.StartYear >= model.EndYear)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var userEducation = await _unitOfWork.UserEducation.GetFirstOrDefault(u => u.Id == model.Id && u.UserId == user!.UserId);

            _mapper.Map<UpdateEducationRequest, UserEducation>(model, userEducation!);

            _unitOfWork.UserEducation.UpdateAsync(userEducation!);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEducation(int id)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var userEducation = await _unitOfWork.UserEducation.GetFirstOrDefault(u => u.Id == id && u.UserId == user!.UserId);

            _unitOfWork.UserEducation.Remove(userEducation!);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddEducation(AddEducationRequest model)
        {
            if (model.StartYear >= model.EndYear)
            {
                throw new CustomException(String.Format(Messages.StartDateGreaterThenEndDate));
            }

            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            UserEducation userEducation = new();

            _mapper.Map<AddEducationRequest, UserEducation>(model, userEducation!);

            userEducation.UserId = user!.UserId;
            userEducation.CreatedDate = DateTime.Now;

            await _unitOfWork.UserEducation.CreateAsync(userEducation!);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateSocialProfile(UserSocialProfileRequest model)
        {
            var user = await _unitOfWork.User.GetFirstOrDefault(u => u.AspnetuserId.ToString() == GetUserId());

            var socialProfile = await _unitOfWork.UserSocialProfile.GetFirstOrDefaultNullable(u => u.UserId == user!.UserId);

            if(socialProfile == null)
            {
                UserSocialProfile data = new();

                _mapper.Map<UserSocialProfileRequest, UserSocialProfile>(model, data);

                data.UserId = user!.UserId;
                data.CreatedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map<UserSocialProfileRequest, UserSocialProfile>(model, socialProfile!);

                _unitOfWork.UserSocialProfile.UpdateAsync(socialProfile);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
