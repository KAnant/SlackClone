using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SlackApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Services
{
    public class MailSender
    {

        public string FromName { get; set; }

        public string FromEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string AuthEmail { get; set; }
        public string AuthPassword { get; set; }
        private readonly IOptions<MailSettings> _mailSettings;

        public void SetupMailSettings(IOptions<MailSettings> mailSettings)
        {
            Host = mailSettings.Value.Host;
            Port = mailSettings.Value.Port;
            AuthEmail = mailSettings.Value.AuthEmail;
            AuthPassword = mailSettings.Value.AuthPassword;
        }

        public void SendMail(List<string> to, string subject, string body, IOptions<MailSettings> mailSettings, List<string> ccMailAddress = null)
        {
            FromName = FromName ?? "Slack Admin";
            FromEmail = FromEmail ?? "SlackAdmin@slack.com";
            try
            {
                SetupMailSettings(mailSettings);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(FromName, FromEmail));

                InternetAddressList sentToList = new InternetAddressList();
                foreach (string email in to)
                {
                    sentToList.Add(new MailboxAddress(email));
                }
                InternetAddressList ccToList = new InternetAddressList();
                if (ccMailAddress != null)
                {
                    foreach (string cc in ccMailAddress)
                    {
                        ccToList.Add(new MailboxAddress(cc));
                    }
                }
                message.Cc.AddRange(ccToList);
                message.To.AddRange(sentToList);
                message.Subject = subject;
                message.Body = new MimeKit.TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(Host, Port, true);
                    client.Authenticate(AuthEmail, AuthPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

