using System;
using System.Security.Claims;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Auth.Abstractions;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace Fs.Api.Infrastructure.Services
{
    internal sealed class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _context;

        public UserContextService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetUserId()
        {
            return new(_context.HttpContext.User.FindFirst(JwtClaimTypes.Id).Value);
        }

        public UserRole GetUserRole()
        {
            var role = _context.HttpContext.User.FindFirst(JwtClaimTypes.Role) ?? _context.HttpContext.User.FindFirst(ClaimTypes.Role);
            return (UserRole)Enum.Parse(typeof(UserRole), role.Value, true);
        }
    }
}
