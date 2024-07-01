using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user_education")]
public partial class UserEducation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("degree_id")]
    public int? DegreeId { get; set; }

    [Column("institute_name", TypeName = "character varying")]
    public string InstituteName { get; set; } = null!;

    [Column("percentage/grade", TypeName = "character varying")]
    public string PercentageGrade { get; set; } = null!;

    [Column("start_year")]
    public int StartYear { get; set; }

    [Column("end_year")]
    public int EndYear { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("DegreeId")]
    [InverseProperty("UserEducations")]
    public virtual DegreeType? Degree { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserEducations")]
    public virtual User User { get; set; } = null!;
}
