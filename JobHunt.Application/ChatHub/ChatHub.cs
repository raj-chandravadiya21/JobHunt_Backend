using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

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

        public async Task SendMessage(string conversationId, string senderId, string content) 
        {
            Message message = new()
            {
                ConversationId = int.Parse(conversationId),
                SenderId = int.Parse(senderId),
                Content = content,
                CreatedDate = DateTime.Now,
                Seen = false,
            };

            await _unitOfWork.Message.CreateAsync(message);
            await _unitOfWork.SaveAsync();

            await Clients.Group(conversationId).SendAsync("ReceiveMessage", new
            {
                SenderId = senderId,
                Content = content,
                CreatedDate = DateTime.Now,
            });
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
    }
}
