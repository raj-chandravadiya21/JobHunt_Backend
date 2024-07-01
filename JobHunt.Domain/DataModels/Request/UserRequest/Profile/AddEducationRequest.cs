using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class AddEducationRequest
    {
        [Required]
        public int? DegreeId { get; set; }

        [Required]
        public string InstituteName { get; set; } = null!;

        [Required]
        public string PercentageGrade { get; set; } = null!;

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }
    }
}
