using System;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pep
{
    public sealed class EnforceUpdateUser :
        BasePolicyEnforcementRequest<BaseEnforcementSubject, UpdateUserResource>
    {
    }

    public record UpdateUserResource
    {
        public Guid UserId { get; }
        public bool IsUpdateProfile { get; }

        public UpdateUserResource(Guid userId, bool isUpdateProfile)
        {
            UserId = userId;
            IsUpdateProfile = isUpdateProfile;
        }
    }
}