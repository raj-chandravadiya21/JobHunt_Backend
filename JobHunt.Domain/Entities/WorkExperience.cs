using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("work_experience")]
public partial class WorkExperience
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("job_title")]
    [StringLength(100)]
    public string? JobTitle { get; set; }

    [Column("company_name")]
    [StringLength(100)]
    public string CompanyName { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("WorkExperiences")]
    public virtual User User { get; set; } = null!;
}
