using System;
using Fs.Domain.Aggregates.UserAggregate;
using MediatR;

namespace Fs.Infrastructure.Permissions.Abstractions
{
    public interface IEnforcePolicyRequest<out TResponse> : IRequest<TResponse>
    {
        void SetBasicSubject(Guid userId, UserRole userRole);
    }
}