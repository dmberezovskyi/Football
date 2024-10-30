using FluentValidation;
using Fs.Application.Commands.Base;

namespace Fs.Application.Commands.EmailCommands
{
    public sealed class TrySendEmailCommand
        : BaseCommand
    {
    }
    public class TrySendEmailCommandValidator : AbstractValidator<TrySendEmailCommand>
    {
        public TrySendEmailCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Version).GreaterThan(0);
        }
    }
}
