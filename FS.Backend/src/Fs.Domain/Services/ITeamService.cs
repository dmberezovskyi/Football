using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fs.Domain.Services
{
    public interface ITeamService
    {
        Task<bool> IsNameExistsAsync(Guid organizationId, string name, CancellationToken cancellationToken);
        Task<(string Name, Guid? OrganizationId)?> GetDetailsAsync(Guid id, CancellationToken cancellationToken);
    }
}
