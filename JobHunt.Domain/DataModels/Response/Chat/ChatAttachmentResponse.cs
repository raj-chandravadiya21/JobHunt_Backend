namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class ChatAttachmentResponse
    {
        public string Url { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; } = 0;

        public string FileType { get; set; } = string.Empty;    

        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
