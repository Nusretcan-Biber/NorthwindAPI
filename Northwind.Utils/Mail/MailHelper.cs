using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.Mail
{
    public class MailHelper : IMailHelper
    {
        private readonly SmtpClient smtpClient;

        public MailHelper(IConfiguration configuration)
        {
            smtpClient = new SmtpClient();
        }
        public async Task SendMailAsync(string subject, string body, string receptients)
        {
            if (smtpClient.IsConnected)
            {
                await smtpClient.DisconnectAsync(true);
            }
            await smtpClient.ConnectAsync("smtp.gmail.com",587,false);
            smtpClient.Authenticate("nusretcanbiber1@gmail.com", "qcvscydqghviflro");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nusretcanbiber1@gmail.com"));
            email.To.AddRange(InternetAddressList.Parse(receptients));
            email.Subject= subject;
            email.Body= new TextPart(TextFormat.Plain) { Text = body };
            await smtpClient.SendAsync(email);

            if (smtpClient.IsConnected)
            {
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
