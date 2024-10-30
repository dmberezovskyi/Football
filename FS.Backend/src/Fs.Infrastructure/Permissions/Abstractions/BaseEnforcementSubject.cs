using System;
using Fs.Domain.Aggregates.UserAggregate;

namespace Fs.Infrastructure.Permissions.Abstractions
{
    public class BaseEnforcementSubject
    {
        public Guid UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}