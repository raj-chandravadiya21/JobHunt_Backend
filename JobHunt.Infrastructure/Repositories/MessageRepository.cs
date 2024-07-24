using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json;

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

            try
            {
                var result = await _context.ChatModel.FromSqlRaw("SELECT * FROM public.get_chat(@conversationId)", parameter).FirstAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new();
            }
            
        }
    }
}
