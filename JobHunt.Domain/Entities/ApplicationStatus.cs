using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("application_status")]
public partial class ApplicationStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<ApplicationStatusLog> ApplicationStatusLogs { get; set; } = new List<ApplicationStatusLog>();

    [InverseProperty("Status")]
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
