namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserEducationResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? DegreeId { get; set; }

        public string InstituteName { get; set; } = null!;

        public string PercentageGrade { get; set; } = null!;

        public string? Streem { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }
    }
}
