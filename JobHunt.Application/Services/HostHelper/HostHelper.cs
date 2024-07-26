using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.IHostHelper;
using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobHunt.Application.Services.HostHelper
{
    public class HostHelper(IServiceProvider serviceProvider, IEmailSender emailSender) : IHostHelper
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IEmailSender _emailSender = emailSender;

        public async Task SendDailyNotification()
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var users = await unitOfWork.AspNetUser.WhereList(u => u.RoleId == (int)Role.User);
            var emails = users.Select(u => u.Email);

            foreach (var email in emails)
            {
                await _emailSender.DailyNotificationEmail(email);
            }
        }

        public async Task SendEmailForUnseenMessages()
        {
            using var scope =  _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            List<UnseenChatModel> unseenMessage = await unitOfWork.Message.GetUnseenMessages();

            if(unseenMessage == null)
            {
                return;
            }

            foreach(var message in unseenMessage)
            {
                string name = message.JobName == null ? message.SenderName : $"{message.SenderName} For {message.JobName}";

                await _emailSender.SendUnseenMessageNotification(message.MessageCount, name, message.RecipientEmail);

                var notifications = message.MessageIds!.Select(messageId => new MessageNotification
                {
                    MessageId = messageId,
                    AspnetuserId = message.RecipientId,
                    SentDate = DateTime.Now
                }).ToList();

                await unitOfWork.MessageNotification.AddRangeAsync(notifications);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
