using System;

namespace Fs.Domain.Exceptions.Email
{
    public sealed class EmailNotReadyBeToSentException : Exception
    {
        public EmailNotReadyBeToSentException()
        {
        }
        public EmailNotReadyBeToSentException(string message)
            : base(message)
        {
        }
        public EmailNotReadyBeToSentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
