using System.Collections.Generic;

namespace Fs.Api.Infrastructure.Error
{
    public sealed class ErrorViewModel
    {
        public string Reason { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Location { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}
