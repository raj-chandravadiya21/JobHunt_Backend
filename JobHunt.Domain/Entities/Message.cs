using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("message")]
public partial class Message
{
    [Key]
    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("conversation_id")]
    public int ConversationId { get; set; }

    [Column("sender_id")]
    public int SenderId { get; set; }

    [Column("content", TypeName = "character varying")]
    public string Content { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("seen")]
    public bool Seen { get; set; }

    [ForeignKey("ConversationId")]
    [InverseProperty("Messages")]
    public virtual Conversation Conversation { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("Messages")]
    public virtual Aspnetuser Sender { get; set; } = null!;
}
