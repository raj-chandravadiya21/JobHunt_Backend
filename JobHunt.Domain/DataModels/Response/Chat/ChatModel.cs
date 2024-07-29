namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class ChatModel
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public bool Seen { get; set; }
    }
}
