namespace JobHunt.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailVerifiaction(int otp, string email);

        Task SendResetPasswordLink(string token,string email);

        Task DailyNotificationEmail(string email);

        Task SendUnseenMessageNotification(double messageCount, string name, string email);
    }
}
