using System.Collections.Generic;

namespace Fs.Application.Validation
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public string Location { get; }

        public Dictionary<string, object> Values { get; }

        public Error(string code, string message, string location, Dictionary<string, object> values = null)
        {
            Code = code;
            Message = message;
            Location = location;
            Values = values;
        }
    }
}
