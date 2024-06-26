using JobHunt.Application.Interfaces;
using JobHunt.Application.Interfaces.IHostHelper;
using Microsoft.Extensions.Hosting;

namespace JobHunt.Application.HostServices
{
    public class DailyEmailSender(IHostHelper hostHelper) : BackgroundService
    {
        private readonly IHostHelper _hostHelper = hostHelper;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //await _hostHelper.SendDailyNotification();

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
