using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Permissions.Pdp;
using Fs.Application.Permissions.User.Pdp;
using Fs.Infrastructure.Permissions;
using Fs.Infrastructure.Permissions.Abstractions;
using MediatR;

namespace Fs.Application.Permissions.User.Pep
{
    internal sealed class PolicyEnforcementPoint :
        IRequestHandler<EnforceListUsers>
    {
        private readonly UserPolicyDecisionPoint _decisionPoint;
        private readonly ListUsersPolicyDecisionPoint _listUsersPdp;

        public PolicyEnforcementPoint(UserPolicyDecisionPoint decisionPoint)
        {
            _decisionPoint = decisionPoint;
            _listUsersPdp = new ListUsersPolicyDecisionPoint();
        }
        public async Task<Unit> Handle(EnforceListUsers request, CancellationToken cancellationToken)
        {
            if(await _listUsersPdp.EvaluateDecisionAsync(request.Subject, request.Resource) == Decision.Deny)
                throw new ActionDeniedException();

            return Unit.Value;
        }
    }
}
