using FluentValidation;
using Fs.Application.Commands.Base;

namespace Fs.Application.Commands.EmailCommands
{
    public sealed class RenderEmailCommand
        : BaseCommand
    {
    }

    public class RenderEmailCommandValidator : AbstractValidator<RenderEmailCommand>
    {
        public RenderEmailCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Version).GreaterThan(0);
        }
    }
}
