using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Application.Permissions.Pep;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;

namespace Fs.Application.Commands.UserCommands
{
    public sealed class UserCommandHandler : BaseCommandHandler,
        IRequestHandler<RegisterUserCommand>,
        IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashGenerator _passwordHashGenerator;
        private readonly IMediator _mediator;
        private readonly ITeamService _teamService;

        public UserCommandHandler(IRepository repository, IUserService userService, IPasswordHashGenerator passwordHashGenerator, IMediator mediator, ITeamService teamService)
            : base(repository)
        {
            _userService = userService;
            _passwordHashGenerator = passwordHashGenerator;
            _mediator = mediator;
            _teamService = teamService;
        }

        public async Task<Unit> Handle(RegisterUserCommand cmd, CancellationToken cancellationToken)
        {
            var user = await User.RegisterUserAsync(cmd.Id, cmd.Email, _userService, cancellationToken);
            user.ChangePassword(cmd.Password, _passwordHashGenerator);
            user.ChangeRole(Enum.Parse<UserRole>(cmd.Role, true));
            user.UpdateProfile(new Profile(cmd.FirstName, cmd.MiddleName, cmd.LastName, cmd.BirthDate, null, cmd.Phone, null));

            Add(user);

            return Unit.Value;
        }
        public async Task<Unit> Handle(UpdateUserCommand cmd, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EnforceUpdateUser
            {
                Resource = new UpdateUserResource(
                    userId: cmd.Id,
                    isUpdateProfile: cmd.Profile != null)
            }, cancellationToken);

            var user = await GetAsync<User>(cmd.Id, cmd.Version, cancellationToken);

            if (cmd.Profile != null)
            {
                user.UpdateProfile(new Profile(
                    cmd.Profile.FirstName,
                    cmd.Profile.MiddleName,
                    cmd.Profile.LastName,
                    cmd.Profile.BirthDate,
                    cmd.Profile.About,
                    cmd.Profile.Phone,
                    cmd.Profile.Address));
            }

            if (cmd.Team != null)
                await user.ChangeTeamAsync(cmd.Team.TeamId, _teamService, cancellationToken);

            return Unit.Value;
        }
    }
}