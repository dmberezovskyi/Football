using System;
using System.Security.Claims;
using FluentValidation;
using Fs.Application.Commands.Base;
using Fs.Application.Commands.UserCommands;
using Fs.Application.Extensions;
using Fs.Application.Validation;
using IdentityServer4.Services;

namespace Fs.Application.Commands.AuthCommands
{
    public sealed class LogInCommand
        : BaseCommand<LogInResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }

    public sealed class LogInResponse
    {
        public string RedirectUrl { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }

    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        private readonly IIdentityServerInteractionService _idsrvInteractionService;

        public LogInCommandValidator(IIdentityServerInteractionService idsrvInteractionService)
        {
            _idsrvInteractionService = idsrvInteractionService;

            RuleFor(x => x.Email).IsEmail();
            RuleFor(x => x.Password).IsPassword();
            RuleFor(x => x.ReturnUrl).NotEmpty().MustAsync(async (returnUrl, cancellationToken) =>
            {
                var (authorizationRequest, redirectUrl) = await _idsrvInteractionService.GetAuthContextAsync(returnUrl);
                return authorizationRequest != null;
            }).WithMessage("Invalid 'returnUrl' value. The authorization context could not be found.");
        }
    }
}
