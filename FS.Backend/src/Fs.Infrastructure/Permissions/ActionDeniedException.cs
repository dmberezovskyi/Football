using System;

namespace Fs.Infrastructure.Permissions
{
    public sealed class ActionDeniedException : Exception
    {
        public ActionDeniedException()
        {
        }
        public ActionDeniedException(string message)
            : base(message)
        {
        }
        public ActionDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
