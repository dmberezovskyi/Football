using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fs.Application.Permissions.Abstractions
{
    internal interface IPolicyInformationPoint
    {
        Task<Guid?> GetUserOrganizationId(Guid userId, CancellationToken cancellationToken);
    }
}
