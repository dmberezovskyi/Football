using System;
using FluentValidation;
using Fs.Application.Validation;

namespace Fs.Application.Queries.Users
{
    public sealed class UsersQuery : IQuery<UserReadModel>
    {
        public Guid[] Ids { get; set; }
        public string[] Parts { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }

    public class UsersQueryValidator : AbstractValidator<UsersQuery>
    {
        public UsersQueryValidator()
        {
            RuleFor(x => x.Ids).IsLengthLessThanOrEqualTo(50);
            RuleFor(x => x.Parts).NotNull().IsInArray(Constants.User.Query.Parts.All);
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take).InclusiveBetween(1, 50);
        }
    }
}
