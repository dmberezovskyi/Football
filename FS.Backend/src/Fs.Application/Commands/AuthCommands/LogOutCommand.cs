using FluentValidation;
using Fs.Application.Commands.Base;

namespace Fs.Application.Commands.AuthCommands
{
    public sealed class LogOutCommand
        : BaseCommand
    {
        public string LogoutId { get; set; }
    }

    public class LogOutCommandValidator : AbstractValidator<LogOutCommand>
    {
        public LogOutCommandValidator()
        {
            RuleFor(x => x.LogoutId).NotEmpty();
        }
    }
}
