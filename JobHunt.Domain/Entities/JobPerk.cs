using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("job_perks")]
public partial class JobPerk
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("job_id")]
    public int JobId { get; set; }

    [Column("perks")]
    [StringLength(50)]
    public string Perks { get; set; } = null!;

    [ForeignKey("JobId")]
    [InverseProperty("JobPerks")]
    public virtual Job Job { get; set; } = null!;
}
