using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Infrastructure.Extensions;
using Fs.Infrastructure.Storage;
using MediatR;

namespace Fs.Application.Behaviors.Command
{
    public class DomainEventsBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;

        public DomainEventsBehaviour(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            await _mediator.DispatchDomainEventsAsync(_dbContext);

            return response;
        }
    }
}
