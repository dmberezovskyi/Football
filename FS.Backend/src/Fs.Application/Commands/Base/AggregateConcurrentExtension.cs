using System;

namespace Fs.Application.Commands.Base
{
    public class AggregateConcurrentExtension : Exception
    {
        public AggregateConcurrentExtension()
        {
        }
        public AggregateConcurrentExtension(string message)
            : base(message)
        {
        }
        public AggregateConcurrentExtension(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
