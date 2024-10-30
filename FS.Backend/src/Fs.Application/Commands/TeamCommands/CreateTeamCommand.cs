using System;
using FluentValidation;
using Fs.Application.Commands.Base;

namespace Fs.Application.Commands.TeamCommands
{
    public sealed class CreateTeamCommand
        : BaseCommand
    {
        public string Name { get; set; }
        public Guid? OrganizationId { get; set; }
    }

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(v => v.Name).IsTeamName();
            RuleFor(v => v.OrganizationId).NotEmpty().When(x => x.OrganizationId != null);
        }
    }
}
