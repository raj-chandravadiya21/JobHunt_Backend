using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("roleset")]
public partial class Roleset
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<Aspnetuser> Aspnetusers { get; set; } = new List<Aspnetuser>();
}
