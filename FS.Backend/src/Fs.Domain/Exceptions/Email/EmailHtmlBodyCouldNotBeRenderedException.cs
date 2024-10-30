using System;

namespace Fs.Domain.Exceptions.Email
{
    public sealed class EmailHtmlBodyCouldNotBeRenderedException : Exception
    {
        public EmailHtmlBodyCouldNotBeRenderedException()
        {
        }
        public EmailHtmlBodyCouldNotBeRenderedException(string message)
            : base(message)
        {
        }
        public EmailHtmlBodyCouldNotBeRenderedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
