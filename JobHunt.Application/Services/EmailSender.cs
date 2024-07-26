using JobHunt.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace JobHunt.Application.Services
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var senderEmail = configuration["EmailService:SenderEmail"]!;
            var password = configuration["EmailService:Password"]!;

            var mail = new MailMessage(senderEmail, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, password)
            };

            await client.SendMailAsync(mail);
        }

        public async Task SendEmailVerifiaction(int otp, string email)
        {
            var subject = "Email Verification OTP For JobHunt";

            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            var filePath = Path.Combine(parentDirectory!, "JobHunt.Domain", "EmailTemplate", "email-verification.html");

            var htmlTemplate = ReadHtmlTemplateFromFile(filePath);

            var htmlBody = htmlTemplate.Replace("{{otp}}", otp.ToString());

            await SendEmailAsync(email, subject, htmlBody);
        }

        public async Task SendResetPasswordLink(string token, string email)
        {
            var subject = "Reset Your Account Password";

            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            var filePath = Path.Combine(parentDirectory!, "JobHunt.Domain", "EmailTemplate", "reset-password.html");

            var htmlTemplate = ReadHtmlTemplateFromFile(filePath);

            var resetLink = $"http://localhost:5173/reset-password/{token}";

            var htmlBody =  htmlTemplate.Replace("{{action_url}}", resetLink);

            await SendEmailAsync(email, subject, htmlBody);
        }

        public async Task DailyNotificationEmail(string email)
        {
            var subject = "Notification From JobHunt";

            var message = "Daily Email Check";

            await SendEmailAsync(email, subject, message);
        }

        public string ReadHtmlTemplateFromFile(string filePath)
        {
            string htmlBody = string.Empty;

            htmlBody = File.ReadAllText(filePath);

            return htmlBody;
        }

        public async Task SendUnseenMessageNotification(double messageCount, string name, string email)
        {
            var subject = "You've Got a New Message!";

            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            var filePath = Path.Combine(parentDirectory!, "JobHunt.Domain", "EmailTemplate", "unseen_notification.html");

            var htmlTemplate = ReadHtmlTemplateFromFile(filePath);

            var htmlBody = htmlTemplate.Replace("{{message_count}}", messageCount.ToString()).Replace("{{name}}", name);

            await SendEmailAsync(email, subject, htmlBody);
        }

    }
}
