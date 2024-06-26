using JobHunt.Domain.Entities;
using System.Collections;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Registration
{
    public class RegistrationUserRequest
    {
        public string? Contact { get; set; }

        public DateOnly? DOB { get; set; }

        public string? Gender { get; set; }

        public string? Experience { get; set; }

        public string? Address { get; set; }

        public List<int> Skills { get; set; } = new List<int>();

        public List<int> Languages { get; set; } = new List<int>();

        public string? Photo { get; set; }

        public List<EducationDetailsUserModel> EducationDetails { get; set; } = new List<EducationDetailsUserModel>();

        public string? LinkedInUrl { get; set; }

        public string? GithubUrl { get; set; }

        public string? WebsiteUrl { get; set; }

        public List<ProjectsUserModel> Projects { get; set; } = new List<ProjectsUserModel>();

        public List<WorkExperienceUserModel> WorkExperience { get; set; } = new List<WorkExperienceUserModel>();
    }
}