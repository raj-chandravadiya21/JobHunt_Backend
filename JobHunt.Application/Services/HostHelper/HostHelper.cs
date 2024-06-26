using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.IHostHelper;
using JobHunt.Domain.Enum;
using JobHunt.Infrastructure.Interfaces;
using JobHunt.Infrastructure.Repositories;
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
    }
}
