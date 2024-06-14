using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("otp_record")]
public partial class OtpRecord
{
    [Key]
    [Column("otp_id")]
    public int OtpId { get; set; }

    [Column("otp")]
    public int Otp { get; set; }

    [Column("sent_datetime", TypeName = "timestamp without time zone")]
    public DateTime SentDatetime { get; set; }

    [Column(TypeName = "character varying")]
    public string Email { get; set; } = null!;
}
