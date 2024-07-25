using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("job")]
public partial class Job
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("job_name")]
    [StringLength(50)]
    public string JobName { get; set; } = null!;

    [Column("location")]
    [StringLength(300)]
    public string Location { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("ctc_start")]
    public int CtcStart { get; set; }

    [Column("ctc_end")]
    public int CtcEnd { get; set; }

    [Column("experience_in_years")]
    public double ExperienceInYears { get; set; }

    [Column("last_date_to_apply")]
    public DateOnly LastDateToApply { get; set; }

    [Column("no_of_openings")]
    public int? NoOfOpenings { get; set; }

    [Column("job_description")]
    [StringLength(5000)]
    public string JobDescription { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("modify_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifyDate { get; set; }

    [Column("requirements")]
    [StringLength(5000)]
    public string Requirements { get; set; } = null!;

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("close_date")]
    public DateOnly? CloseDate { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Jobs")]
    public virtual Company Company { get; set; } = null!;

    [InverseProperty("Job")]
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    [InverseProperty("Job")]
    public virtual ICollection<JobPerk> JobPerks { get; set; } = new List<JobPerk>();

    [InverseProperty("Job")]
    public virtual ICollection<JobResponsibility> JobResponsibilities { get; set; } = new List<JobResponsibility>();

    [InverseProperty("Job")]
    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
