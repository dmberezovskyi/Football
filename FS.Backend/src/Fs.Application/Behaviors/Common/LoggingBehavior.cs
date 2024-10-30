using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fs.Application.Behaviors.Common
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var cmdName = request.GetType().Name;
            using (_logger.BeginScope(new
            {
                Command = cmdName,
            }))
            {

                try
                {
                    _logger.LogInformation($"Handling request: {cmdName}");
                    var response = await next();
                    _logger.LogInformation($"Request handled: {cmdName}");

                    return response;
                }
                catch (ValidationException)
                {
                    throw;
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc, $"Failed '{request.GetType().Name}' request processing");
                    throw;
                }
            }
        }
    }
}
