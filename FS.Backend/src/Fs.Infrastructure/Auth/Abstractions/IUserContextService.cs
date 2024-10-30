using System;
using Fs.Domain.Aggregates.UserAggregate;

namespace Fs.Infrastructure.Auth.Abstractions
{
    public interface IUserContextService
    {
        public Guid GetUserId();
        UserRole GetUserRole();
    }
}
