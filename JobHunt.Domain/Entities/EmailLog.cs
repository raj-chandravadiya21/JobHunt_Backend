using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("email_log")]
public partial class EmailLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    [StringLength(50)]
    public string From { get; set; } = null!;

    [Column("to")]
    [StringLength(50)]
    public string To { get; set; } = null!;

    [Column("subject")]
    [StringLength(200)]
    public string Subject { get; set; } = null!;

    [Column("createddate", TypeName = "timestamp without time zone")]
    public DateTime Createddate { get; set; }
}
