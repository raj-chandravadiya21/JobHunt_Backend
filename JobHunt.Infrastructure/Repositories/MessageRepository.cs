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

        public async Task<ChatModel> GetChat(int conversationId)
        {
            var parameter = new NpgsqlParameter[]
            {
                new("@conversationId", conversationId)
            };

            return await _context.ChatModel.FromSqlRaw("SELECT * FROM public.get_chat(@conversationId)", parameter).FirstAsync();
        }

        public async Task<List<UnseenChatModel>> GetUnseenMessages() =>
             await _context.UnseenChatModel.FromSqlRaw("SELECT * FROM public.get_unseen_message()").ToListAsync();
    }
}
