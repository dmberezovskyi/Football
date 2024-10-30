using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Application.Permissions.Pep;
using Fs.Domain.Aggregates.OrganizationAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Permissions.Abstractions;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;

namespace Fs.Application.Commands.OrganizationCommands
{
    public sealed class OrganizationCommandHandlers : BaseCommandHandler,
        IRequestHandler<CreateOrganizationCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrganizationService _organizationService;

        public OrganizationCommandHandlers(IRepository repository, IMediator mediator, IOrganizationService organizationService)
            : base(repository)
        {
            _mediator = mediator;
            _organizationService = organizationService;
        }

        public async Task<Unit> Handle(CreateOrganizationCommand cmd, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EnforceCreateOrganization
            {
                Resource = new BaseEnforcementResource(cmd.Id)
            }, cancellationToken);

            var organization = await Organization.CreateAsync(cmd.Id, cmd.Name, cmd.Address, _organizationService, cancellationToken);

            Add(organization);

            return Unit.Value;
        }
    }
}
