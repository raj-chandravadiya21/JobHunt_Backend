namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserProjectResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = null!;

        public string? Url { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Description { get; set; }
    }
}
