namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class MessageModel
    {
        public string ContentType { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ThumbnailUrl { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; } = 0;
    }
}
