using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Permissions.Abstractions;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pdp
{
    internal sealed class TeamPolicyDecisionPoint
    {
        private readonly IPolicyInformationPoint _pip;

        public TeamPolicyDecisionPoint(IPolicyInformationPoint pip)
        {
            _pip = pip;
        }

        public async Task<Decision> EvaluateCreateDecisionAsync(Guid userId, UserRole userRole, Guid? teamOrganizationId, CancellationToken cancellationToken)
        {
            switch (userRole)
            {
                case UserRole.Admin:
                    return Decision.Permit;
                case UserRole.Player:
                    return Decision.Deny;
                case UserRole.Trainer:
                case UserRole.Director:
                    if (!teamOrganizationId.HasValue)
                        return Decision.Permit;

                    var userOrganizationId = await _pip.GetUserOrganizationId(userId, cancellationToken);
                    if (userOrganizationId.HasValue && userOrganizationId.Value == teamOrganizationId.Value)
                        return Decision.Permit;

                    return Decision.Deny;
                default:
                    return Decision.Deny;
            }
        }
    }
}
