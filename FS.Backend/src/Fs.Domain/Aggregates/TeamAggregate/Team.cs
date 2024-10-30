using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Exceptions.Organization;
using Fs.Domain.Exceptions.Team;
using Fs.Domain.SeedWork;
using Fs.Domain.Services;

namespace Fs.Domain.Aggregates.TeamAggregate
{
    public sealed class Team : BaseEntity,
        IAggregateRoot
    {
        public string Name { get; private set; }
        public Guid? OrganizationId { get; private set; }

        private Team(Guid id)
            : base(id) { }
        private Team() { }
        public static async Task<Team> CreateAsync(Guid id, string name, Guid? organizationId, ITeamService teamService, IOrganizationService organizationService, CancellationToken cancellationToken)
        {
            var team = new Team(id);

            if (organizationId.HasValue && !await organizationService.IsExistsAsync(organizationId.Value, cancellationToken))
                throw new OrganizationNotFoundException("The organization not found.");

            team.OrganizationId = organizationId;
            await team.UpdateNameAsync(name, teamService, cancellationToken);

            return team;
        }

        public async Task UpdateNameAsync(string name, ITeamService teamService, CancellationToken cancellationToken)
        {
            if (OrganizationId.HasValue && await teamService.IsNameExistsAsync(OrganizationId.Value, name, cancellationToken))
                throw new TeamNameAlreadyExistsException("The team name already exists in the organization.");

            Name = name.Trim();
        }
    }
}