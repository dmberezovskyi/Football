using System;
using Fs.Domain.Aggregates.UserAggregate;
using MediatR;

namespace Fs.Infrastructure.Permissions.Abstractions
{
    public abstract class BasePolicyEnforcementRequest<TSubject, TResource> : IEnforcePolicyRequest<Unit>
        where TSubject : BaseEnforcementSubject, new()
    {
        public TSubject Subject { get; set; }
        public TResource Resource { get; set; }
        
        public void SetBasicSubject(Guid userId, UserRole userRole)
        {
            Subject ??= new TSubject();

            Subject.UserId = userId;
            Subject.UserRole = userRole;
        }
    }
}