using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("job_responsibility")]
public partial class JobResponsibility
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("job_id")]
    public int JobId { get; set; }

    [Column("responsibility")]
    [StringLength(5000)]
    public string? Responsibility { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("JobResponsibilities")]
    public virtual Job Job { get; set; } = null!;
}
