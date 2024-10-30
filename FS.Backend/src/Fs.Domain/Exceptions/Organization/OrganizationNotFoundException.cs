using System;

namespace Fs.Domain.Exceptions.Organization
{
    public sealed class OrganizationNotFoundException : Exception
    {
        public OrganizationNotFoundException()
        {
        }
        public OrganizationNotFoundException(string message)
            : base(message)
        {
        }
        public OrganizationNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
