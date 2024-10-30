using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Fs.Domain.Services;
using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.EmailServices.Smtp
{
    internal class SmtpService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpOptions _options;

        public SmtpService(IOptions<SmtpOptions> options)
        {
            _options = options.Value;

            _smtpClient = new SmtpClient(_options.Host, _options.Port)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(_options.UserName, _options.Password),
                EnableSsl = _options.UseSsl
            };
        }

        public async Task SendAsync(string recipient, string subject, string htmlBody)
        {
            using var message = new MailMessage()
            {
                From = new MailAddress(_options.From, "Footbase"),
                To = { recipient },
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = htmlBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };

            
            await _smtpClient.SendMailAsync(message);
        }
    }
}
