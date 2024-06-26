using JobHunt.Application.Services.HostHelper;

namespace JobHunt.Application.Interfaces.IHostHelper
{
    public interface IHostHelper 
    {
        Task SendDailyNotification();
    }
}
