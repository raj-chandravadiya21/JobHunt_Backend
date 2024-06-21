using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Request
{
    public class ValidateTokenDTO
    {

        [Required]
        public string? Token { get; set; }
    }
}
