namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class UpdateWorkExperience
    {
        public int Id { get; set; }

        public string JobTitle { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Description { get; set; }
    }
}
