using System.Threading.Tasks;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pdp
{
    internal sealed class OrganizationPolicyDecisionPoint
    {
        public Task<Decision> EvaluateDecisionAsync(BaseEnforcementSubject subject, BaseEnforcementResource resource)
        {
            return Task.FromResult(subject.UserRole == UserRole.Admin
                ? Decision.Permit
                : Decision.Deny);
        }
    }
}