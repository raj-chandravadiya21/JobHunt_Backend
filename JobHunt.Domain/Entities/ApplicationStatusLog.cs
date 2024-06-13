using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("application_status_logs")]
public partial class ApplicationStatusLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("notes")]
    [StringLength(500)]
    public string Notes { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("ApplicationStatusLogs")]
    public virtual Application Application { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("ApplicationStatusLogs")]
    public virtual ApplicationStatus Status { get; set; } = null!;
}
