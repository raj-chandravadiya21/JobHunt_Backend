using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("application")]
public partial class Application
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("job_id")]
    public int JobId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("applied_date")]
    public DateOnly AppliedDate { get; set; }

    [Column("resume")]
    [StringLength(100)]
    public string? Resume { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<ApplicationStatusLog> ApplicationStatusLogs { get; set; } = new List<ApplicationStatusLog>();

    [InverseProperty("Application")]
    public virtual ICollection<InterviewDetail> InterviewDetails { get; set; } = new List<InterviewDetail>();

    [ForeignKey("JobId")]
    [InverseProperty("Applications")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Applications")]
    public virtual ApplicationStatus Status { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Applications")]
    public virtual User User { get; set; } = null!;
}
