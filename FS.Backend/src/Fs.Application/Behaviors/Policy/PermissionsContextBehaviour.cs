using System.Threading;
using System.Threading.Tasks;
using Fs.Infrastructure.Auth.Abstractions;
using Fs.Infrastructure.Permissions.Abstractions;
using MediatR;

namespace Fs.Application.Behaviors.Policy
{
    public class PermissionsContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IEnforcePolicyRequest<TResponse>
    {
        private readonly IUserContextService _userContextService;

        public PermissionsContextBehaviour(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            request.SetBasicSubject(_userContextService.GetUserId(), _userContextService.GetUserRole());
            return await next();
        }
    }
}
