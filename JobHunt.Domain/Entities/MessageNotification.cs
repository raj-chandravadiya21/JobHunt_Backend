using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("message_notification")]
public partial class MessageNotification
{
    [Key]
    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("aspnetuser_id")]
    public int AspnetuserId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime SentDate { get; set; }

    [ForeignKey("AspnetuserId")]
    [InverseProperty("MessageNotifications")]
    public virtual Aspnetuser Aspnetuser { get; set; } = null!;

    [ForeignKey("MessageId")]
    [InverseProperty("MessageNotifications")]
    public virtual Message Message { get; set; } = null!;
}
