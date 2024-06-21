using JobHunt.Domain.Entities;
using System.Collections;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Registration
{
    public class RegistrationUserDTO
    {
        public string? Token { get; set; }

        public string? Contact { get; set; }

        public DateOnly? DOB { get; set; }

        public string? Gender { get; set; }

        public string? Experience { get; set; }

        public string? Address { get; set; }

        public List<int> Skills { get; set; } = new List<int>();

        public List<int> Languages { get; set; } = new List<int>();

        public string? Photo { get; set; }

        public List<EducationDetailsUserDTO> EducationDetails { get; set; } = new List<EducationDetailsUserDTO>();

        public string? LinkedInUrl { get; set; }

        public string? GithubUrl { get; set; }

        public string? WebsiteUrl { get; set; }

        public List<ProjectsUserDTO> Projects { get; set; } = new List<ProjectsUserDTO>();

        public List<WorkExperienceUserDTO> WorkExperience { get; set; } = new List<WorkExperienceUserDTO>();
    }
}