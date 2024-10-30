using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.EmailCommands;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Domain.Events.User;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;

namespace Fs.Application.DomainEventHandlers.UserRegistered
{
    public sealed class SendRegistrationConfirmationEmail
        : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IMediator _mediator;
        private readonly IRepository _repository;
        private readonly FrontEndOptions _frontEndOptions;

        public SendRegistrationConfirmationEmail(IMediator mediator, IRepository repository, IOptions<FrontEndOptions> frontEndOptions)
        {
            _mediator = mediator;
            _repository = repository;
            _frontEndOptions = frontEndOptions.Value;
        }

        public async Task Handle(UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById<User>(@event.Id, cancellationToken);

            await _mediator.Send(new CreateEmailCommand
            {
                Recipient = user.Email,
                TemplateName = "EmailConfirmation",
                CultureName = "en-US",
                TemplateData = new
                {
                    User = new
                    {
                        
                    },
                    ConfirmationLink = string.Format(_frontEndOptions.EmailConfirmationUrl, user.EmailConfirmationToken)
                }
            }, cancellationToken);
        }
    }
}