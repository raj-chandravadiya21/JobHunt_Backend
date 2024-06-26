using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailVerifiaction(int otp, string email);

        Task SendResetPasswordLink(string token,string email);

        Task DailyNotificationEmail(string email);
    }
}
