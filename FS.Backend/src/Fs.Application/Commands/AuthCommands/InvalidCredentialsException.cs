using System;

namespace Fs.Application.Commands.AuthCommands
{
    public sealed class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }
        public InvalidCredentialsException(string message)
            : base(message)
        {
        }
        public InvalidCredentialsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
