using System;

namespace Fs.Domain.Exceptions.Email
{
    public sealed class EmailAlreadyRenderedException : Exception
    {
        public EmailAlreadyRenderedException()
        {
        }
        public EmailAlreadyRenderedException(string message)
            : base(message)
        {
        }
        public EmailAlreadyRenderedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
