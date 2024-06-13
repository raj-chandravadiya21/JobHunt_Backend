using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Keyless]
[Table("user_languages")]
public partial class UserLanguage
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [ForeignKey("LanguageId")]
    public virtual Language Language { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
