namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class UserSocialProfileRequest
    {
        public string LinkendinUrl { get; set; } = null!;

        public string GithubUrl { get; set; } = null!;

        public string WebsiteUrl { get; set; } = null!;
    }
}
