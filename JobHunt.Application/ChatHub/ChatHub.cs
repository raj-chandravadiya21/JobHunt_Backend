using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace JobHunt.Application.ChatHub
{
    public class ChatHub(IUnitOfWork unitOfWork) : Hub
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var conversationId = httpContext.Request.Query["conversationId"];
            if (!string.IsNullOrEmpty(conversationId))
            {
                LogHelper.LogInformation($"Connection On : {conversationId}");
                await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var conversationId = httpContext.Request.Query["conversationId"];
            if (!string.IsNullOrEmpty(conversationId))
            {
                LogHelper.LogInformation($"Connection Disconnect on : {conversationId}");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string conversationId, string senderId, string content, string contentType, string? fileName = null, string? thumbnailUrl = null, long fileSize = 0) 
        {
            var contentObject = new
            {
                contentType,
                content,
                thumbnailUrl,
                fileName,
                fileSize,
            };

            Message message = new()
            {
                ConversationId = int.Parse(conversationId),
                SenderId = int.Parse(senderId),
                Content = JsonSerializer.Serialize(contentObject),
                CreatedDate = DateTime.Now,
                Seen = false,
            };


            await _unitOfWork.Message.CreateAsync(message);
            await _unitOfWork.SaveAsync();

            ChatModel chatModel = new()
            {
                MessageId = message.MessageId,
                SenderId = int.Parse(senderId),
                Content = message.Content,
                CreatedDate = message.CreatedDate,
                Seen = message.Seen
            };

            await Clients.Group(conversationId).SendAsync("ReceiveMessage", chatModel);
        }

        public async Task MarkMessagesAsSeen(string conversationId, string userId)
        {
            var messages = await _unitOfWork.Message.WhereList(m => m.ConversationId == int.Parse(conversationId) && m.SenderId != int.Parse(userId) && !m.Seen);
            foreach (var message in messages)
            {
                message.Seen = true;
                _unitOfWork.Message.UpdateAsync(message);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Message>> GetUnseenMessages(string conversationId, string userId)
        {
            return await _unitOfWork.Message.WhereList(m => m.ConversationId == int.Parse(conversationId) && m.SenderId != int.Parse(userId) && !m.Seen);
        }
        public async Task Typing(string conversationId, string senderId)
        {
            await Clients.Group(conversationId).SendAsync("Typing", senderId);
        }

        public async Task StopTyping(string conversationId, string senderId)
        {
            await Clients.Group(conversationId).SendAsync("StopTyping", senderId);
        }
    }
}
