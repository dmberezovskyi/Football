using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.EmailCommands;
using Fs.Domain.Aggregates.EmailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fs.BackgroundTasks.Services
{
    public sealed class EmailRenderService : BaseBackgroundService
    {
        protected override TimeSpan Interval => TimeSpan.FromSeconds(5);

        public EmailRenderService(ILogger<EmailSenderService> logger, IServiceScopeFactory scopeFactory)
            : base("EmailRenderer", logger, scopeFactory)
        {
        }

        protected override async Task ProcessAsync(CancellationToken stoppingToken)
        {
            do
            {
                var email = await Repository.Get<Email>(x => x.Status == EmailStatus.New)
                    .Select(x => new {x.Id, x.Version})
                    .FirstOrDefaultAsync(stoppingToken);

                if (email == null)
                    return;

                await Mediator.Send(new RenderEmailCommand()
                {
                    Id = email.Id,
                    Version = email.Version
                }, stoppingToken);
            } while (true);
        }
    }
}