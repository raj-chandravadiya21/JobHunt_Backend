using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobHunt.Infrastructure.Repositories
{
    public class MessageRepository(DefaultdbContext context) : Repository<Message>(context), IMessageRepository
    {
        private readonly DefaultdbContext _context = context;

        public async Task<List<ChatModel>> GetChat(int conversationId, int pageNumber, int pageSize)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@conversationId", conversationId),
                new("@pageNumber", pageNumber),
                new("@pageSize", pageSize)
            };

            return await _context.ChatModel.FromSqlRaw("SELECT * FROM public.get_chats(@conversationId, @pageNumber, @pageSize)", parameter).ToListAsync();
        }

        public async Task<List<UnseenChatModel>> GetUnseenMessages() =>
             await _context.UnseenChatModel.FromSqlRaw("SELECT * FROM public.get_unseen_message()").ToListAsync();
    }
}
