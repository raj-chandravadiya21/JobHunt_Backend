using JobHunt.Domain.DataModels.Response.Chat;
using Microsoft.AspNetCore.Http;

namespace JobHunt.Application.Interfaces.ChatInterface
{
    public interface IChatService
    {
        Task<List<ChatModel>> GetMessage(int conversationId, int pageNumber, int pageSize);

        Task<ChatAttachmentResponse> Upload(IFormFile file, string thumbnail);
    }
}
