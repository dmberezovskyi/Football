using System.Linq;
using Fs.Infrastructure.ReadStorage.Abstractions;
using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fs.Infrastructure.ReadStorage
{
    internal class ReadRepository : IReadRepository
    {
        private readonly ReadDbContext _context;

        public ReadRepository(ReadDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>()
            where T : BaseEntity
        {
            return _context.Set<T>().AsNoTracking();
        }
    }
}
