namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserSocialProfileResponse
    {
        public int UserId { get; set; }

        public string? LinkendinUrl { get; set; }

        public string? GithubUrl { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
