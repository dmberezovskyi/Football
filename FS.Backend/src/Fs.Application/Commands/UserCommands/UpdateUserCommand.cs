using System;
using FluentValidation;
using Fs.Application.Commands.Base;
using Fs.Application.Models;
using Fs.Application.Validation;

namespace Fs.Application.Commands.UserCommands
{
    public sealed class UpdateUserCommand
        : BaseCommand
    {
        public ProfileDto Profile { get; init; }
        public TeamDto Team { get; init; }

        public sealed record ProfileDto
        {
            public string FirstName { get; init; }
            public string MiddleName { get; init; }
            public string LastName { get; init; }
            public DateTime BirthDate { get; init; }
            public string Phone { get; init; }
            public Address Address { get; init; }
            public string About { get; init; }
        }
        public sealed record TeamDto
        {
            public Guid? TeamId { get; init; }
        }
    }

    public class UpdateUserCommandValidator : BaseCommandValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Version).GreaterThan(0);

            RuleFor(x => x.Profile.FirstName).IsFirstName().When(HasProfile);
            RuleFor(x => x.Profile.MiddleName).IsMiddleName().When(HasProfile);
            RuleFor(x => x.Profile.LastName).IsLastName().When(HasProfile);
            RuleFor(x => x.Profile.BirthDate).IsBirthDate().When(HasProfile);
            RuleFor(x => x.Profile.Phone).IsPhone().When(HasProfile);
            RuleFor(x => x.Profile.Address).IsAddress().When(HasProfile);
            RuleFor(x => x.Profile.About).IsUserAbout().When(HasProfile);

            RuleFor(x => x.Team.TeamId).NotEmpty().When(HasTeam);
        }

        private static bool HasProfile(UpdateUserCommand cmd)
        {
            return cmd.Profile != null;
        }
        private static bool HasTeam(UpdateUserCommand cmd)
        {
            return cmd.Team != null;
        }
    }
}