using Microsoft.Extensions.Options;

namespace Fs.Application
{
    public sealed class FrontEndOptions : IOptions<FrontEndOptions>
    {
        public string BaseUrl { get; set; }
        public string EmailConfirmationUrl { get; set; }

        FrontEndOptions IOptions<FrontEndOptions>.Value => this;
    }
}
