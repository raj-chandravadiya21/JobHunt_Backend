using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public int Role { get; set; }
    }
}
