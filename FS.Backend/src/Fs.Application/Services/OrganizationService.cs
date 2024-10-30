using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Aggregates.OrganizationAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Services
{
    internal sealed class OrganizationService : IOrganizationService
    {
        private readonly IRepository _repository;

        public OrganizationService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.Get<Organization>(x => x.Id == id).AnyAsync(cancellationToken);
        }

        public Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken)
        {
            name = name.Trim();
            return _repository.Get<Organization>(x => x.Name == name).AnyAsync(cancellationToken);
        }
    }
}
