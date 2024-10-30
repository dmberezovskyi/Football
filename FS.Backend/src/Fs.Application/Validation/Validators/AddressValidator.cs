using FluentValidation;
using Fs.Application.Models;

namespace Fs.Application.Validation.Validators
{
    internal class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Country).NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(32);
            RuleFor(x => x.State).NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(32);
            RuleFor(x => x.City).NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(32);
            RuleFor(x => x.ZipCode).NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(12);
            RuleFor(x => x.StreetAddress).NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(32);
        }
    }
}