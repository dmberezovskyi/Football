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
    public sealed class EmailSenderService : BaseBackgroundService
    {
        protected override TimeSpan Interval => TimeSpan.FromSeconds(5);

        public EmailSenderService(ILogger<EmailSenderService> logger, IServiceScopeFactory scopeFactory)
            : base("EmailSender", logger, scopeFactory)
        {
        }

        protected override async Task ProcessAsync(CancellationToken stoppingToken)
        {
            do
            {
                var email = await Repository.Get<Email>(x => x.Status == EmailStatus.ReadyToSend)
                    .Select(x => new { x.Id, x.Version })
                    .FirstOrDefaultAsync(stoppingToken);

                if (email == null)
                    return;

                await Mediator.Send(new TrySendEmailCommand
                {
                    Id = email.Id,
                    Version = email.Version
                }, stoppingToken);
            } while (true);
        }
    }
}