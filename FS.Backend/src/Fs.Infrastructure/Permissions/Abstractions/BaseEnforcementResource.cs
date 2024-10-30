using System;

namespace Fs.Infrastructure.Permissions.Abstractions
{
    public class BaseEnforcementResource
    {
        public Guid Id { get; set; }

        public BaseEnforcementResource()
        {
        }
        public BaseEnforcementResource(Guid id)
        {
            Id = id;
        }
    }
}