using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.EmailServices.Smtp
{
    public sealed class SmtpOptions : IOptions<SmtpOptions>
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }

        SmtpOptions IOptions<SmtpOptions>.Value => this;
    }
}
