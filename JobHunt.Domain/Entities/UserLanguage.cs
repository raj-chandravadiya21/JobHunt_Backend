using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user_languages")]
public partial class UserLanguage
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Key]
    [Column("user_language_id")]
    public int UserLanguageId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserLanguages")]
    public virtual User User { get; set; } = null!;
}
