using JobHunt.Application.Interfaces.ChatInterface;
using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Infrastructure.Interfaces;
using System.Text.Json;

namespace JobHunt.Application.Services.ChatService
{
    public class ChatServices(IUnitOfWork unitOfWork) : IChatService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ChatResponse> GetMessage(int conversatioId)
        {
            var data = await _unitOfWork.Message.GetChat(conversatioId);

             var messages = JsonSerializer.Deserialize<List<MessageModel>>(data.Messages);

            ChatResponse response = new()
            {
                ConversationId = data.ConversationId,
                JobName= data.JobName,
                CompanyName = data.CompanyName,
                UserName = data.UserName,
                Messages = messages
            };

            return response;
        }
    }
}
