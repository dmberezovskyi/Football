using FluentValidation;
using Fs.Application.Commands.Base;
using Fs.Application.Validation;

namespace Fs.Application.Commands.EmailCommands
{
    public sealed class CreateEmailCommand
        : BaseCommand
    {
        public string Recipient { get; set; }
        public string TemplateName { get; set; }
        public dynamic TemplateData { get; set; }
        public string CultureName { get; set; }
    }

    public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
    {
        public CreateEmailCommandValidator()
        {
            RuleFor(x => x.Recipient).IsEmail();
            RuleFor(x => x.TemplateName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.CultureName).NotEmpty().MaximumLength(8);
        }
    }
}
