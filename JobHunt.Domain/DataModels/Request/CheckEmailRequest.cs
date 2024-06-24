using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request
{
    public class CheckEmailRequest
    {
        [Required]
        public string? Email { get; set; }
    }
}
