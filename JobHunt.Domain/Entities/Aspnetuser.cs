using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("aspnetuser")]
public partial class Aspnetuser
{
    [Key]
    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(30)]
    public string? LastName { get; set; }

    [Column("password", TypeName = "character varying")]
    public string Password { get; set; } = null!;

    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("Aspnetuser")]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [InverseProperty("Aspnetuser")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [ForeignKey("RoleId")]
    [InverseProperty("Aspnetusers")]
    public virtual Roleset? Role { get; set; }

    [InverseProperty("Aspnetuser")]
    public virtual ICollection<UserLog> UserLogs { get; set; } = new List<UserLog>();

    [InverseProperty("Aspnetuser")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
