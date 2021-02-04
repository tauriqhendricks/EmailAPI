using EmailAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;

namespace EmailAPI.Services
{
    public class EmailMessageService
    {

        private readonly EmailSettings _settings;

        public EmailMessageService(IOptions<EmailSettings> settings)
        {
            this._settings = settings.Value;
        }

        public string SendMessage(EmailModel model)
        {
            string from = model.From;
            string body = model.Body;

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(this._settings.From);
            mm.To.Add(this._settings.To);
            mm.Subject = "Please contact " + from;
            mm.Body = body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(this._settings.From, this._settings.Key);

            try
            {
                smtp.Send(mm);
                return "Email sent!!";
            }
            catch (Exception)
            {
                return "Email was not sent!!";
            }
        }
    }
}
