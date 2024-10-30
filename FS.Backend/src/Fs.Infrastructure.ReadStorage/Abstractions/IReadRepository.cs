using System.Linq;
using Fs.Infrastructure.ReadStorage.Entities;

namespace Fs.Infrastructure.ReadStorage.Abstractions
{
    public interface IReadRepository
    {
        IQueryable<T> GetAll<T>()
            where T : BaseEntity;
    }
}
