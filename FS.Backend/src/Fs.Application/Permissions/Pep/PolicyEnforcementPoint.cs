using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Permissions.Pdp;
using Fs.Infrastructure.Permissions;
using Fs.Infrastructure.Permissions.Abstractions;
using MediatR;

namespace Fs.Application.Permissions.Pep
{
    internal sealed class PolicyEnforcementPoint :
        IRequestHandler<EnforceCreateTeam>,
        IRequestHandler<EnforceCreateOrganization>,
        IRequestHandler<EnforceUpdateUser>
    {
        private readonly OrganizationPolicyDecisionPoint _pdp;
        private readonly TeamPolicyDecisionPoint _teamPdp;
        private readonly UserPolicyDecisionPoint _userPdp;

        public PolicyEnforcementPoint(TeamPolicyDecisionPoint teamPdp, UserPolicyDecisionPoint userPdp)
        {
            _teamPdp = teamPdp;
            _userPdp = userPdp;
            _pdp = new OrganizationPolicyDecisionPoint();
        }

        public async Task<Unit> Handle(EnforceCreateTeam request, CancellationToken cancellationToken)
        {
            var decision = await _teamPdp.EvaluateCreateDecisionAsync(
                request.Subject.UserId,
                request.Subject.UserRole,
                request.Resource.OrganizationId,
                cancellationToken);

            if (decision == Decision.Deny)
                throw new ActionDeniedException();

            return Unit.Value;
        }
        public async Task<Unit> Handle(EnforceCreateOrganization request, CancellationToken cancellationToken)
        {
            if (await _pdp.EvaluateDecisionAsync(request.Subject, request.Resource) == Decision.Deny)
                throw new ActionDeniedException();

            return Unit.Value;
        }
        public Task<Unit> Handle(EnforceUpdateUser request, CancellationToken cancellationToken)
        {
            //if (_userPdp.EvaluateUpdateDecision(request.Subject.UserId, request.Subject.UserRole, request.Resource.IsUpdateProfile) == Decision.Deny)
            //    throw new ActionDeniedException();

            return Task.FromResult(Unit.Value);
        }
    }
}
