using System;

namespace Fs.Domain.Exceptions.Organization
{
    public sealed class OrganizationNameAlreadyExistsException : Exception
    {
        public OrganizationNameAlreadyExistsException()
        {
        }
        public OrganizationNameAlreadyExistsException(string message)
            : base(message)
        {
        }
        public OrganizationNameAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
