using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApi.Data.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SlackApi.Controllers
{
    [EnableCors("SlackCorsPolicy")]
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        public SmtpConfig SmtpConfig { get; }
        public MailController(IOptions<SmtpConfig> smtpConfig)
        {
            SmtpConfig = smtpConfig.Value;
        }
        [HttpPost]
        [Route("sendEmail")]
        public ActionResult SendEmail(EmailAdd objModelMail)
        {
            if (ModelState.IsValid)
            {
                string from = "anantkarn98@gmail.com"; //Email like- anil.singh581@gmail.com
                using (MailMessage mail = new MailMessage(from, objModelMail.To))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "a9843368280k");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);

                    //ViewBag.Message = "EmailSent";

                    return Ok(smtp);
                }
            }
            else
            {
                return Ok();
            }
        }
        //public async Task<IActionResult> SendMail([FromBody]Email email)
        //{
        //    var client = new System.Net.Mail.SmtpClient("smtp.example.com", 111);
        //    client.UseDefaultCredentials = false;
        //    client.EnableSsl = true;

        //    client.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");

        //    var mailMessage = new System.Net.Mail.MailMessage();
        //    mailMessage.From = new System.Net.Mail.MailAddress("youremail@example.com");

        //    mailMessage.To.Add(email.Name);

        //    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

        //    await client.SendMailAsync(mailMessage);

        //    return Ok();
        //}

    }
}
