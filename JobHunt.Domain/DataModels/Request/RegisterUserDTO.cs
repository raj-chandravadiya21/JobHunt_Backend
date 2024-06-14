using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request
{
    public class RegisterUserDTO
    {

        [Required]
        //[RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Enter a valid email.")]
        public string? Email { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string? Password { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }

        [Required]
        //[RegularExpression(@"^[^\d]+$", ErrorMessage = "First name cannot contain numbers")]
        public string? FirstName { get; set; }

        [Required]
        //[RegularExpression(@"^[^\d]+$", ErrorMessage = "Last name cannot contain numbers")]
        public string? LastName { get; set; }

        [Required]
        public int Otp { get; set; }
    }
}
