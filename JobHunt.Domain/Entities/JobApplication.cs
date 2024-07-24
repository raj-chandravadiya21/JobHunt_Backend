using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("job_application")]
public partial class JobApplication
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

    [Column("resume")]
    [StringLength(100)]
    public string? Resume { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("applied_date", TypeName = "timestamp without time zone")]
    public DateTime AppliedDate { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<ApplicationStatusLog> ApplicationStatusLogs { get; set; } = new List<ApplicationStatusLog>();

    [InverseProperty("JobApplication")]
    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    [InverseProperty("Application")]
    public virtual ICollection<InterviewDetail> InterviewDetails { get; set; } = new List<InterviewDetail>();

    [ForeignKey("JobId")]
    [InverseProperty("JobApplications")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("JobApplications")]
    public virtual ApplicationStatus Status { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("JobApplications")]
    public virtual User User { get; set; } = null!;
}
