using System.Net;
using System.Net.Mail;

namespace AngularLoginApi.services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task sendEmail(string fromEmail, string message)
        {
            var settings = _config.GetSection("EmailServiceSettings");
            var mail = new MailMessage();
            mail.From = new MailAddress(settings["SenderEmail"]);
            mail.To.Add(settings["SenderEmail"]);
            mail.Subject = "Contact us form subject";
            mail.Body = $@"
New Contact Message

Email   : {fromEmail}
Date    : {DateTime.Now}

Message:
--------------------------------
{message}
--------------------------------
";
            mail.IsBodyHtml = false;

            var smtp = new SmtpClient(settings["smtpServer"])
            {
                Port = int.Parse(settings["Port"]),
                Credentials = new NetworkCredential(
                    settings["SenderEmail"],
                    settings["Password"]
                    ),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
        }
    }
}
