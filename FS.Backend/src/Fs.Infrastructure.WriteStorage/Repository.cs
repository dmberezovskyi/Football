using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.SeedWork;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Infrastructure.Storage
{
    internal class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
            where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
        }
        public async Task<T> GetById<T>(Guid id, CancellationToken cancellationToken)
            where T : BaseEntity
        {
            return _context.Set<T>().Local.FirstOrDefault(x => x.Id == id)
                   ?? await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate)
            where T : BaseEntity
        {
            return _context.Set<T>().Where(predicate);
        }
        public void Delete<T>(T entity)
            where T : BaseEntity
        {
            _context.Remove(entity);
        }
    }
}
