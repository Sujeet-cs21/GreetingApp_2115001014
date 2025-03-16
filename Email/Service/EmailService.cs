using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using NLog;

namespace Email.Service
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            logger.Info("EmailService - SendEmailAsync - Sending email.");
            using var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
            {
                Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"]),
                EnableSsl = true
            };

            logger.Info("EmailService - SendEmailAsync - SmtpClient created.");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["SMTP:Username"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            logger.Info("EmailService - SendEmailAsync - MailMessage created.");
            mailMessage.To.Add(to);

            logger.Info("EmailService - SendEmailAsync - Sending email.");
            await client.SendMailAsync(mailMessage);
        }
    }
}
