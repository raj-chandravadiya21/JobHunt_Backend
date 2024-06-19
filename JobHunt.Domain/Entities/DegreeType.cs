﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("degree_type")]
public partial class DegreeType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    public int Type { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Degree")]
    public virtual ICollection<UserEducation> UserEducations { get; set; } = new List<UserEducation>();
}
