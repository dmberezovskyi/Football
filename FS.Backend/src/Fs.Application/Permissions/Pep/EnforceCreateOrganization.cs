using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pep
{
    public sealed class EnforceCreateOrganization :
        BasePolicyEnforcementRequest<BaseEnforcementSubject, BaseEnforcementResource>
    {
    }
}
