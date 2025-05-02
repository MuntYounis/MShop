using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace MShop.API.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("muntaseryounis60@gmail.com", "jfru lyzv ldlj ybqs")
            };

            return client.SendMailAsync(
                new MailMessage(from: "muntaseryounis60@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml = true
                }
                );
        }
    }
}

//https://tutorials.eu/how-to-send-emails-in-asp-net-web-applications/