using JobHunt.Domain.DataModels.Response.Chat;

namespace JobHunt.Application.Interfaces.ChatInterface
{
    public interface IChatService
    {
        Task<List<ChatModel>> GetMessage(int conversatioId);
    }
}
