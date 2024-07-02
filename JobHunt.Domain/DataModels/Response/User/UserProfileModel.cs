namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserProfileModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Photo { get; set; } = null!;

        public Double? Experience { get; set; }

        public string Gender { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public List<int> Skills { get; set; } = null!;

        public List<int> Languages { get; set; } = null!;
    }
}
