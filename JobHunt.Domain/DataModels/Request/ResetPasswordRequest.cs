using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }
    }
}
