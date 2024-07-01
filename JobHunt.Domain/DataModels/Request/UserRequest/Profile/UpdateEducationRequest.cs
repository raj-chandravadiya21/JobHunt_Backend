using System.ComponentModel.DataAnnotations.Schema;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class UpdateEducationRequest
    {
        public int Id { get; set; }

        public int? DegreeId { get; set; }

        public string InstituteName { get; set; } = null!;

        public string PercentageGrade { get; set; } = null!;

        public int StartYear { get; set; }

        public int EndYear { get; set; }
    }
}
