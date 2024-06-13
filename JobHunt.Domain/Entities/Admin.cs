using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("admin")]
public partial class Admin
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string Phone { get; set; } = null!;

    [Column("address")]
    [StringLength(500)]
    public string? Address { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("created_by")]
    [StringLength(100)]
    public string CreatedBy { get; set; } = null!;

    [Column("modified_by")]
    [StringLength(100)]
    public string? ModifiedBy { get; set; }

    [ForeignKey("AspnetuserId")]
    [InverseProperty("Admins")]
    public virtual Aspnetuser Aspnetuser { get; set; } = null!;
}
