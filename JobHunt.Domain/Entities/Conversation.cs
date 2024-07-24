using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

[Table("conversation")]
public partial class Conversation
{
    [Key]
    [Column("conversation_id")]
    public int ConversationId { get; set; }

    [Column("job_application_id")]
    public int JobApplicationId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("ConversationCompanies")]
    public virtual Aspnetuser Company { get; set; } = null!;

    [ForeignKey("JobApplicationId")]
    [InverseProperty("Conversations")]
    public virtual JobApplication JobApplication { get; set; } = null!;

    [InverseProperty("Conversation")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey("UserId")]
    [InverseProperty("ConversationUsers")]
    public virtual Aspnetuser User { get; set; } = null!;
}
