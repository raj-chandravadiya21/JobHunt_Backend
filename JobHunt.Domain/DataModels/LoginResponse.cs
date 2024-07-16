namespace JobHunt.Domain.DataModels
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public bool IsRegister { get; set; }

        public string? Photo { get; set; }   
    }
}
