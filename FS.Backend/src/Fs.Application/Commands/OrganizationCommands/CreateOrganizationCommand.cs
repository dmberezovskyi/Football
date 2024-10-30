using FluentValidation;
using Fs.Application.Commands.Base;
using Fs.Application.Models;
using Fs.Application.Validation;

namespace Fs.Application.Commands.OrganizationCommands
{
    public sealed class CreateOrganizationCommand
        : BaseCommand
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationCommandValidator()
        {
            RuleFor(v => v.Name).IsOrganizationName();
            RuleFor(x => x.Address).IsAddress();
        }
    }
}
