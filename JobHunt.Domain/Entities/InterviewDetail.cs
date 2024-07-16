using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("interview_details")]
public partial class InterviewDetail
{
    [Key]
    [Column("interview_id")]
    public int InterviewId { get; set; }

    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Column("interview_date")]
    public DateOnly InterviewDate { get; set; }

    [Column("location")]
    [StringLength(500)]
    public string? Location { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("start_time")]
    public TimeOnly? StartTime { get; set; }

    [Column("end_time")]
    public TimeOnly? EndTime { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("InterviewDetails")]
    public virtual JobApplication Application { get; set; } = null!;
}
