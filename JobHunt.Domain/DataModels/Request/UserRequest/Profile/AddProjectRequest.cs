using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class AddProjectRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
