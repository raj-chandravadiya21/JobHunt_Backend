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
            var message = $"<h2> Verify Your Email For JobHunt </h2>" +
                $"<p style=\"font-weight: bold;\">Hey We are excited to have you onboard at JobHunt" +
                $"<p style=\"font-weight: bold;\"> Verify your email address by using the following One-Time-Password(valid for 20 mins):{otp}";

            await SendEmailAsync(email, subject, message);
        }

    }
}
