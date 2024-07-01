namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserWorkExperience
    {
        public int Id { get; set; } 

        public int UserId { get; set; }

        public string? JobTitle { get; set; }

        public string CompanyName { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Description { get; set; }
    }
}
