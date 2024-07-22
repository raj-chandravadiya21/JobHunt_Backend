namespace JobHunt.Domain.DataModels.Response.User.JobApplication
{
    public class ResumeResponse
    {
        public byte[]? FileBytes { get; set; }

        public string? ContentType { get; set; }

        public string? FileName { get; set; }
    }
}
