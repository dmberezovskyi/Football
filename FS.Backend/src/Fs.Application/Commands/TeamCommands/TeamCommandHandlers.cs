using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Application.Permissions.Pep;
using Fs.Domain.Aggregates.TeamAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;

namespace Fs.Application.Commands.TeamCommands
{
    public sealed class TeamCommandHandlers : BaseCommandHandler,
        IRequestHandler<CreateTeamCommand>
    {
        private readonly IMediator _mediator;
        private readonly ITeamService _teamService;
        private readonly IOrganizationService _organizationService;

        public TeamCommandHandlers(IRepository repository, IMediator mediator, ITeamService teamService, IOrganizationService organizationService)
            : base(repository)
        {
            _mediator = mediator;
            _teamService = teamService;
            _organizationService = organizationService;
        }

        public async Task<Unit> Handle(CreateTeamCommand cmd, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EnforceCreateTeam
            {
                Resource = new CreateTeamResource(cmd.OrganizationId)
            }, cancellationToken);

            var team = await Team.CreateAsync(cmd.Id, cmd.Name, cmd.OrganizationId, _teamService,
                _organizationService, cancellationToken);

            Add(team);

            return Unit.Value;
        }
    }
}
