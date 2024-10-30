using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.SeedWork;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Commands.Base
{
    public abstract class BaseCommandHandler
    {
        private readonly IRepository _repository;

        protected BaseCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<T> GetAsync<T>(Guid id, int version, CancellationToken cancellationToken)
            where T : BaseEntity, IAggregateRoot
        {
            var item = await _repository.GetById<T>(id, cancellationToken);

            if (item == null)
                return null;

            if (item.Version != version)
                throw new AggregateConcurrentExtension();

            return item;
        }

        public async Task<bool> ExistsAsync<T>(Guid id, CancellationToken cancellationToken)
            where T : BaseEntity, IAggregateRoot
        {
            return await _repository.Get<T>(x=>x.Id == id).AnyAsync(cancellationToken);
        }

        public void Add<T>(T aggregate)
            where T : BaseEntity, IAggregateRoot
        {
            _repository.Add(aggregate);
        }
    }
}
