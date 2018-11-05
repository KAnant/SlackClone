using Microsoft.Extensions.Options;
using SlackApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Services
{
    public class MailService
    {

        private readonly MailSender _mailSender;
        private readonly IOptions<MailSettings> _mailSettings;
        public MailService(MailSender mailSender, IOptions<MailSettings> mailsettings)
        {
            _mailSender = mailSender;
            _mailSettings = mailsettings;
        }
        public void SendInvitation(string[] email)
        {
            List<string> emailList = new List<string>();
            foreach (var userEmail in email)
            {
                emailList.Add(userEmail);
            }
            string subject = "Slack Invitation";
            string body = $"ADMIN: <br> You are invited to join the slack team insightworkshop : Please open link below <br> https://mail.google.com/mail/u/0/h/1p508u5kur0rs/?&th=1644655909941152&v=c&st=200";
            _mailSender.SendMail(emailList, subject, body, _mailSettings);
        }
    }
}
