using System.Net.Mail;
using System.Net;
using GPS.API.Interfaces;

namespace GPS.API.Services.EmailServices
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        } 

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpServer = _configuration["EmailConfiguration:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailConfiguration:SmtpPort"]);
            var smtpUsername = _configuration["EmailConfiguration:SmtpUsername"];
            var smtpPassword = _configuration["EmailConfiguration:SmtpPassword"];

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }

        public async Task SendEmailWithPdfAsync(string email, string subject, string message, byte[] pdfBytes)
        {
            var smtpServer = _configuration["EmailConfiguration:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailConfiguration:SmtpPort"]);
            var smtpUsername = _configuration["EmailConfiguration:SmtpUsername"];
            var smtpPassword = _configuration["EmailConfiguration:SmtpPassword"];

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            string fileName = $"shift_for_{DateTime.Now:yyyy_MM_dd}.pdf";

            using (var pdfStream = new MemoryStream(pdfBytes))
            {
                var attachment = new Attachment(pdfStream, fileName, "application/pdf");
                mailMessage.Attachments.Add(attachment);

                await client.SendMailAsync(mailMessage);
            }


        }
    }
}
