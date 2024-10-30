using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Extensions;
using Fs.Domain.Services;
using Fs.Infrastructure.ReadStorage.Abstractions;
using Fs.Infrastructure.ReadStorage.Entities;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Commands.AuthCommands
{
    public sealed class AuthCommandHandler :
        IRequestHandler<LogInCommand, LogInResponse>,
        IRequestHandler<LogOutCommand>
    {
        private readonly IReadRepository _repository;
        private readonly IIdentityServerInteractionService _idsrvInteractionService;
        private readonly IEventService _events;
        private readonly IPasswordHashGenerator _passwordHashGenerator;

        public AuthCommandHandler(IReadRepository repository, IIdentityServerInteractionService idsrvInteractionService, IEventService events, IPasswordHashGenerator passwordHashGenerator)
        {
            _repository = repository;
            _idsrvInteractionService = idsrvInteractionService;
            _events = events;
            _passwordHashGenerator = passwordHashGenerator;
        }

        public async Task<LogInResponse> Handle(LogInCommand cmd, CancellationToken cancellationToken)
        {
            var (authorizationRequest, redirectUrl) = await _idsrvInteractionService.GetAuthContextAsync(cmd.ReturnUrl);

            var email = cmd.Email.Trim().ToLowerInvariant();

            var identity = await _repository.GetAll<UserEntity>()
                .Where(x => x.Email == email && x.Status == UserStatus.Active)
                .Select(x => new { x.Id, x.PasswordHash, x.PasswordSalt, x.Role })
                .FirstOrDefaultAsync(cancellationToken);

            if (identity == null)
                throw new InvalidCredentialsException();

            if (identity.PasswordHash != _passwordHashGenerator.Generate(cmd.Password, identity.PasswordSalt))
                throw new InvalidCredentialsException();

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtClaimTypes.Subject, email),
                        new Claim(JwtClaimTypes.Role, identity.Role.ToString()),
                        new Claim(JwtClaimTypes.Id, identity.Id.ToString()),
                        new Claim(JwtClaimTypes.Email, email)
                    },
                    "Bearer",
                    ClaimTypes.Name,
                    ClaimTypes.Role));

            await _events.RaiseAsync(new UserLoginSuccessEvent(email, principal.GetSubjectId(),
                principal.GetDisplayName(), clientId: authorizationRequest.Client.ClientId));

            return new LogInResponse
            {
                RedirectUrl = redirectUrl,
                Principal = principal
            };
        }
        public async Task<Unit> Handle(LogOutCommand cmd, CancellationToken cancellationToken)
        {
            await _idsrvInteractionService.GetLogoutContextAsync(cmd.LogoutId);
            return Unit.Value;
        }
    }
}
