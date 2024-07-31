namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class ChatResponse
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Seen { get; set; }

        public MessageModel? Contents { get; set; }
    }
}
