using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<ChatModel>> GetChat(int conversationId, int pageNumber, int pageSize);

        Task<List<UnseenChatModel>> GetUnseenMessages();
    }
}
