using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Interfaces;

namespace JobHunt.Infrastructure.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<ChatModel> GetChat(int conversationId);

        Task<List<UnseenChatModel>> GetUnseenMessages();
    }
}
