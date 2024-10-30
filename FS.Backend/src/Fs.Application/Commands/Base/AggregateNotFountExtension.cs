using System;

namespace Fs.Application.Commands.Base
{
    public class AggregateNotFountExtension : Exception
    {
        public AggregateNotFountExtension()
        {
        }
        public AggregateNotFountExtension(string message)
            : base(message)
        {
        }
        public AggregateNotFountExtension(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}