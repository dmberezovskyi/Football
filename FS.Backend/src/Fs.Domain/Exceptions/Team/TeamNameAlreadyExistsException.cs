using System;

namespace Fs.Domain.Exceptions.Team
{
    public sealed class TeamNameAlreadyExistsException : Exception
    {
        public TeamNameAlreadyExistsException()
        {
        }
        public TeamNameAlreadyExistsException(string message)
            : base(message)
        {
        }
        public TeamNameAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
