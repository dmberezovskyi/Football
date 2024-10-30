using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fs.Domain.Services
{
    public interface IOrganizationService
    {
        Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken);
    }
}
