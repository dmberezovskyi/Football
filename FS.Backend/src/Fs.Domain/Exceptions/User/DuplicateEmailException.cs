using System;

namespace Fs.Domain.Exceptions.User
{
    public sealed class DuplicateEmailException : Exception
    {
        public DuplicateEmailException()
        {
        }
        public DuplicateEmailException(string message)
            : base(message)
        {
        }
        public DuplicateEmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
