using System;

namespace Fs.Domain.Exceptions.Email
{
    public sealed class EmailAlreadySentException : Exception
    {
        public EmailAlreadySentException()
        {
        }
        public EmailAlreadySentException(string message)
            : base(message)
        {
        }
        public EmailAlreadySentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
