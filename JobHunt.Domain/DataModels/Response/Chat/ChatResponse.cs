namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class ChatResponse
    {
        public int ConversationId { get; set; }

        public string JobName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public List<MessageModel>? Messages { get; set; }
    }
}
