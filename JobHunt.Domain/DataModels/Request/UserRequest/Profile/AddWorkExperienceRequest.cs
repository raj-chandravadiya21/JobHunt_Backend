using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class AddWorkExperienceRequest
    {
        [Required]
        public string JobTitle { get; set; } = null!;

        [Required]
        public string CompanyName { get; set; } = null!;

        [Required]
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        [Required]
        public string Description { get; set; } = null!;
    }
}
