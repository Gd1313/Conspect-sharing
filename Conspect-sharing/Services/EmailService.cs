using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Conspect_sharing.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
           
            //IConfiguration configuration;
            
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Conspect-sharing", "iluxadcp1@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                await client.AuthenticateAsync("iluxadcp1@gmail.com", "tram4vai");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
