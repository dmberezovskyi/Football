using System;
using FluentValidation;
using Fs.Application.Commands.Base;
using Fs.Application.Validation;
using Fs.Domain.Aggregates.UserAggregate;

namespace Fs.Application.Commands.UserCommands
{
    public sealed class RegisterUserCommand 
        : BaseCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

        public string Role { get; set; }
    }

    public class RegisterUserCommandValidator : BaseCommandValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.Email).IsEmail();
            RuleFor(x => x.Password).IsPassword();

            RuleFor(v => v.FirstName).IsFirstName();
            RuleFor(v => v.MiddleName).IsMiddleName().When(x => x.MiddleName != null);
            RuleFor(v => v.LastName).IsLastName();

            RuleFor(v => v.BirthDate).IsBirthDate();
            RuleFor(v => v.Phone).IsPhone();

            RuleFor(x => x.Role).NotEmpty().IsInArray(UserRole.Player.ToString().ToLowerInvariant(), UserRole.Trainer.ToString().ToLowerInvariant());
        }
    }
}
