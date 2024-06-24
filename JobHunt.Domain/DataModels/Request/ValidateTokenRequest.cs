using System.ComponentModel.DataAnnotations;

namespace JobHunt.Domain.DataModels.Request
{
    public class ValidateTokenRequest
    {

        [Required]
        public string? Token { get; set; }
    }
}
