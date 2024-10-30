using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fs.BackgroundTasks.Services
{
    public abstract class BaseBackgroundService : BackgroundService
    {
        protected abstract TimeSpan Interval { get; }
        protected IRepository Repository { get; private set; }
        protected IMediator Mediator { get; private set; }

        private readonly string _name;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        protected BaseBackgroundService(string name, ILogger logger, IServiceScopeFactory scopeFactory)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceScopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                _logger.LogDebug($"{_name} background task: Starting.");

                stoppingToken.Register(() => _logger.LogDebug($"{_name} background task: Stopping."));

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogDebug($"{_name} background task: Processing.");

                    try
                    {
                        using var serviceScope = _serviceScopeFactory.CreateScope();

                        InitServices(serviceScope.ServiceProvider);

                        await ProcessAsync(stoppingToken);
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError(exc, $"{_name} background task: Processing error.");
                    }

                    await Task.Delay(Interval, stoppingToken);
                }

                _logger.LogDebug($"{_name} background task: Stopped.");
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        protected virtual void InitServices(IServiceProvider serviceProvider)
        {
            Repository = serviceProvider.GetService<IRepository>();
            Mediator = serviceProvider.GetService<IMediator>();
        }
        protected abstract Task ProcessAsync(CancellationToken stoppingToken);
    }
}
