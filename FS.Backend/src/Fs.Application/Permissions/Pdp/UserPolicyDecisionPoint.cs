using System;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pdp
{
    internal sealed class UserPolicyDecisionPoint
    {
        public Decision EvaluateCreateDecision(Guid userId, UserRole userRole)
        {
            return userRole == UserRole.Admin
                ? Decision.Permit
                : Decision.Deny;
        }

        public Decision EvaluateUpdateDecision(Guid currentUserId, UserRole currentUserRole, bool isUpdateProfile, Guid userId)
        {
            return currentUserRole == UserRole.Admin || currentUserId == userId
                ? Decision.Permit
                : Decision.Deny;
        }
    }

    public record UserUpdateParams
    {
        public Guid CurrentUserId { get; set; }
        public UserRole CurrentUserRole { get; set; }
        public bool IsUpdateProfile { get; set; }
    }
}
