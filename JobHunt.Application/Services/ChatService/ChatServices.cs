using JobHunt.Application.Interfaces.ChatInterface;
using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Infrastructure.Interfaces;
using System.Text.Json;

namespace JobHunt.Application.Services.ChatService
{
    public class ChatServices(IUnitOfWork unitOfWork) : IChatService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<List<ChatModel>> GetMessage(int conversatioId) =>
            await _unitOfWork.Message.GetChat(conversatioId);
    }
}
