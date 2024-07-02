using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user")]
public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(30)]
    public string? LastName { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    [Column("createddate", TypeName = "timestamp without time zone")]
    public DateTime Createddate { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("address")]
    [StringLength(500)]
    public string? Address { get; set; }

    [Column("photo")]
    [StringLength(100)]
    public string? Photo { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("country", TypeName = "character varying")]
    public string? Country { get; set; }

    [Column("state", TypeName = "character varying")]
    public string? State { get; set; }

    [Column("city", TypeName = "character varying")]
    public string? City { get; set; }

    [Column("experience")]
    public double? Experience { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [ForeignKey("AspnetuserId")]
    [InverseProperty("Users")]
    public virtual Aspnetuser Aspnetuser { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("User")]
    public virtual ICollection<UserEducation> UserEducations { get; set; } = new List<UserEducation>();

    [InverseProperty("User")]
    public virtual ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();

    [InverseProperty("User")]
    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

    [InverseProperty("User")]
    public virtual ICollection<UserSocialProfile> UserSocialProfiles { get; set; } = new List<UserSocialProfile>();

    [InverseProperty("User")]
    public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
}
