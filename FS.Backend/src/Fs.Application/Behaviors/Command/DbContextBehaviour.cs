using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Infrastructure.Storage;
using MediatR;

namespace Fs.Application.Behaviors.Command
{
    public class DbContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly AppDbContext _dbContext;

        public DbContextBehaviour(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var needSave = !_dbContext.IsWorkStarted;
            _dbContext.IsWorkStarted = true;

            var response = await next();

            if (needSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                _dbContext.IsWorkStarted = false;
            }

            return response;
        }
    }
}
