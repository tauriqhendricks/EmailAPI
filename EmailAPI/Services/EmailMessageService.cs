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
            _settings = settings.Value;
        }

        public EmailModel SendMessage(EmailModel model)
        {
            EmailModel email = new EmailModel();
            email.FullName = model.FullName;
            email.From = model.From;
            email.Body = model.Body;

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(_settings.From);
            mm.To.Add(_settings.To);
            mm.Subject = "Please contact " + email.FullName;
            mm.Body = email.Body + Environment.NewLine + Environment.NewLine + "Email Address: " + email.From;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(_settings.From, _settings.Key);

            try
            {
                smtp.Send(mm);
                return email;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
