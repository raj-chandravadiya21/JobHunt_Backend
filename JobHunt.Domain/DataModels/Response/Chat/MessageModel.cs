namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class MessageModel
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public bool Seen { get; set; }
    }
}
