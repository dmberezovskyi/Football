using Microsoft.Extensions.Options;

namespace Fs.Application.Services.Auth
{
    public class AuthOptions : IOptions<AuthOptions>
    {
        public string BaseUrl { get; set; }
        public string IssuerUri { get; set; }
        public string Origin { get; set; }
        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }

        AuthOptions IOptions<AuthOptions>.Value => this;
    }
}
