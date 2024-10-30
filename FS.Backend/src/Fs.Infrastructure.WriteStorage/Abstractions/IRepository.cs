using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.SeedWork;

namespace Fs.Infrastructure.Storage.Abstractions
{
    public interface IRepository
    {
        void Add<T>(T entity)
            where T : BaseEntity;

        Task<T> GetById<T>(Guid id, CancellationToken cancellationToken)
            where T : BaseEntity;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate)
            where T : BaseEntity;

        void Delete<T>(T entity)
            where T : BaseEntity;
    }
}
