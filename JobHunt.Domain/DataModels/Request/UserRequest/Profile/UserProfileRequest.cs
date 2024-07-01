namespace JobHunt.Domain.DataModels.Request.UserRequest.Profile
{
    public class UserProfileRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Photo { get; set; } = null!;

        public string Experience { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public List<int> Skills { get; set; } = null!;

        public List<int> Languages { get; set; } = null!;
    }
}
