using System;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.User.Pep
{
    public sealed class EnforceListUsers :
        BasePolicyEnforcementRequest<BaseEnforcementSubject, EnforceListUsersResource>
    {
        public EnforceListUsers(EnforceListUsersResource resource)
        {
            Resource = resource;
        }
    }
    public sealed class EnforceListUsersResource
    {
        public Guid[] Ids { get; }
        public string[] Parts { get; }

        public EnforceListUsersResource(Guid[] ids, string[] parts)
        {
            Ids = ids;
            Parts = parts;
        }
    }
}
