namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class UnseenChatModel
    {
        public int ConversationId { get; set; }

        public int RecipientId { get; set; }

        public string RecipientEmail { get; set; } = null!;

        public string SenderName { get; set; } = null!;

        public string? JobName { get; set; }

        public List<int>? MessageIds { get; set; }

        public double MessageCount { get; set; }    
    }
}
