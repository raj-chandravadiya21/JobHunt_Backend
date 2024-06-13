using System;
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

    [Column("naem")]
    [StringLength(50)]
    public string Naem { get; set; } = null!;

    [InverseProperty("Degree")]
    public virtual ICollection<UserEducation> UserEducations { get; set; } = new List<UserEducation>();
}
