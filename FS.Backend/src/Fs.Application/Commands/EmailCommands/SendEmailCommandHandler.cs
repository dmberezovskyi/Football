using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.Base;
using Fs.Domain.Aggregates.EmailAggregate;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using MediatR;

namespace Fs.Application.Commands.EmailCommands
{
    public sealed class SendEmailCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateEmailCommand>,
        IRequestHandler<RenderEmailCommand>,
        IRequestHandler<TrySendEmailCommand>
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHtmlRenderService _htmlRenderService;
        private readonly IEmailService _emailService;

        public SendEmailCommandHandler(IRepository repository, IJsonSerializer jsonSerializer,
            IHtmlRenderService htmlRenderService, IEmailService emailService)
            : base(repository)
        {
            _jsonSerializer = jsonSerializer;
            _htmlRenderService = htmlRenderService;
            _emailService = emailService;
        }

        public Task<Unit> Handle(CreateEmailCommand cmd, CancellationToken cancellationToken)
        {
            var email = new Email(Guid.NewGuid(), cmd.Recipient, cmd.TemplateName, cmd.TemplateData, cmd.CultureName,
                _jsonSerializer);

            Add(email);

            return Task.FromResult(Unit.Value);
        }

        public async Task<Unit> Handle(RenderEmailCommand cmd, CancellationToken cancellationToken)
        {
            var email = await GetAsync<Email>(cmd.Id, cmd.Version, cancellationToken);

            await email.RenderAsync(_htmlRenderService, _jsonSerializer);

            return Unit.Value;
        }

        public async Task<Unit> Handle(TrySendEmailCommand cmd, CancellationToken cancellationToken)
        {
            var email = await GetAsync<Email>(cmd.Id, cmd.Version, cancellationToken);

            await email.TrySendAsync(_emailService);

            return Unit.Value;
        }
    }
}