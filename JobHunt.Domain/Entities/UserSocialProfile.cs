using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user_social_profile			")]
public partial class UserSocialProfile
{
    [Key]
    [Column("user_profile_id")]
    public int UserProfileId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("linkendin_url")]
    [StringLength(100)]
    public string? LinkendinUrl { get; set; }

    [Column("github_url")]
    [StringLength(100)]
    public string? GithubUrl { get; set; }

    [Column("website_url")]
    [StringLength(100)]
    public string? WebsiteUrl { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserSocialProfiles")]
    public virtual User User { get; set; } = null!;
}
