using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Aggregates.TeamAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Services
{
    internal sealed class TeamService : ITeamService
    {
        private readonly IRepository _repository;

        public TeamService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> IsNameExistsAsync(Guid organizationId, string name, CancellationToken cancellationToken)
        {
            name = name.Trim();
            return _repository
                .Get<Team>(x => x.OrganizationId == organizationId && x.Name == name)
                .AnyAsync(cancellationToken);
        }

        public async Task<(string Name, Guid? OrganizationId)?> GetDetailsAsync(Guid id, CancellationToken cancellationToken)
        {
            var r = await _repository.Get<Team>(x => x.Id == id)
                .Select(x => new { x.Name, x.OrganizationId })
                .FirstOrDefaultAsync(cancellationToken);

            if (r == null)
                return null;

            return (r.Name, r.OrganizationId);
        }
    }
}
