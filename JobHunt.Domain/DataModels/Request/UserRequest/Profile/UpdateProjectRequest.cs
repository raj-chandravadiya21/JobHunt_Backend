namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class UpdateProjectRequest
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Url { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Description { get; set; } = null;
    }
}
