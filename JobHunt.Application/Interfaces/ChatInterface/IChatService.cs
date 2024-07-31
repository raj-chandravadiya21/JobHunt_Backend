using JobHunt.Domain.DataModels.Response.Chat;
using Microsoft.AspNetCore.Http;

namespace JobHunt.Application.Interfaces.ChatInterface
{
    public interface IChatService
    {
        Task<List<ChatResponse>> GetMessage(int conversationId);

        Task<ChatAttachmentResponse> Upload(IFormFile file, string thumbnail);
    }
}
