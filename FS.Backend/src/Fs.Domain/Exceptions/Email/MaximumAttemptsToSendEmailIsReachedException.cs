using System;

namespace Fs.Domain.Exceptions.Email
{
    public sealed class MaximumAttemptsToSendEmailIsReachedException : Exception
    {
        public MaximumAttemptsToSendEmailIsReachedException()
        {
        }
        public MaximumAttemptsToSendEmailIsReachedException(string message)
            : base(message)
        {
        }
        public MaximumAttemptsToSendEmailIsReachedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
