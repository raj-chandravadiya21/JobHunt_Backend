using Microsoft.AspNetCore.Http;

namespace JobHunt.Domain.DataModels.Request.UserRequest.Application
{
    public class ApplyJobRequest
    {
        public int JobId { get; set; }

        public IFormFile? Resume { get; set; }

        public string Description { get; set; } = null!;

        public bool IsUploadFromProfile { get; set; }
    }
}
