using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("company")]
public partial class Company
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column("company_name", TypeName = "character varying")]
    public string CompanyName { get; set; } = null!;

    [Column("website", TypeName = "character varying")]
    public string? Website { get; set; }

    [Column("establised_date")]
    public DateOnly? EstablisedDate { get; set; }

    [Column("address", TypeName = "character varying")]
    public string? Address { get; set; }

    [Column("email", TypeName = "character varying")]
    public string Email { get; set; } = null!;

    [Column("logo", TypeName = "character varying")]
    public string? Logo { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("created_by", TypeName = "character varying")]
    public string? CreatedBy { get; set; }

    [Column("modified_by", TypeName = "character varying")]
    public string? ModifiedBy { get; set; }

    [Column("is_approve")]
    public bool IsApprove { get; set; }

    [Column("description", TypeName = "character varying")]
    public string? Description { get; set; }

    [ForeignKey("AspnetuserId")]
    [InverseProperty("Companies")]
    public virtual Aspnetuser Aspnetuser { get; set; } = null!;

    [InverseProperty("Company")]
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
