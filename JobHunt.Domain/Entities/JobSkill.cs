using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Keyless]
[Table("job_skills")]
public partial class JobSkill
{
    [Column("job_id")]
    public int JobId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [ForeignKey("JobId")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("SkillId")]
    public virtual Skill Skill { get; set; } = null!;
}
