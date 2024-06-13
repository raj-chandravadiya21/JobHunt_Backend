using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("user_log	")]
public partial class UserLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column("islogin")]
    public bool Islogin { get; set; }

    [Column("datetime", TypeName = "timestamp without time zone[]")]
    public List<DateTime> Datetime { get; set; } = null!;

    [ForeignKey("AspnetuserId")]
    [InverseProperty("UserLogs")]
    public virtual Aspnetuser Aspnetuser { get; set; } = null!;
}
