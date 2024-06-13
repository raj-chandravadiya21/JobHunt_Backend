using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user_skill")]
public partial class UserSkill
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("UserSkills")]
    public virtual Skill Skill { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserSkill")]
    public virtual User User { get; set; } = null!;
}
