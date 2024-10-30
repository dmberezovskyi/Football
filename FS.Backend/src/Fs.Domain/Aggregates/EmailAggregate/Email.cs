using System;
using System.Threading.Tasks;
using Fs.Domain.Exceptions.Email;
using Fs.Domain.SeedWork;
using Fs.Domain.Services;

namespace Fs.Domain.Aggregates.EmailAggregate
{
    public class Email : BaseEntity,
        IAggregateRoot
    {
        public string Recipient { get; private set; }
        public string TemplateName { get; private set; }
        public string TemplateData { get; private set; }
        public EmailStatus Status { get; private set; }
        public string HtmlBody { get; private set; }
        public string Subject { get; private set; }
        public int AttemptsToSend { get; private set; }
        public string CultureName { get; private set; }

        public Email(Guid id, string recipient, string templateName, object templateData, string cultureName, IJsonSerializer serializer)
            : base(id)
        {
            Recipient = recipient;
            TemplateName = templateName.Trim();
            TemplateData = serializer.Serialize(templateData);
            Status = EmailStatus.New;
            AttemptsToSend = 0;
            CultureName = cultureName;
        }
        protected Email() { }

        public async Task RenderAsync(IHtmlRenderService renderService, IJsonSerializer serializer)
        {
            EnsureNotSent();
            EnsureNotRendered();

            var model = serializer.Deserialize<object>(TemplateData);

            try
            {
                var htmlBodyTask = renderService.RenderAsync($"EmailTemplates/{TemplateName}.Body", model, CultureName);
                var subjectTask = renderService.RenderAsync($"EmailTemplates/{TemplateName}.Subject", model, CultureName);
                await Task.WhenAll(htmlBodyTask, subjectTask);

                HtmlBody = htmlBodyTask.Result;
                Subject = subjectTask.Result;
            }
            catch (Exception exc)
            {
                throw new EmailHtmlBodyCouldNotBeRenderedException("The email HTML body could not be rendered.", exc);
            }

            Status = EmailStatus.ReadyToSend;
        }
        public async Task TrySendAsync(IEmailService emailService)
        {
            EnsureNotSent();
            EnsureNotFailed();
            EnsureReadyToSend();

            try
            {
                await emailService.SendAsync(Recipient, Subject, HtmlBody);
                Status = EmailStatus.Sent;
            }
            catch
            {
                AttemptsToSend++;
                if (AttemptsToSend >= 3)
                    Status = EmailStatus.Failed;
            }
        }

        private void EnsureNotSent()
        {
            if (Status == EmailStatus.Sent)
                throw new EmailAlreadySentException("The email has been already sent.");
        }
        private void EnsureNotFailed()
        {
            if (Status == EmailStatus.Failed)
                throw new MaximumAttemptsToSendEmailIsReachedException("Maximum attempts to send an email is reached.");
        }
        private void EnsureReadyToSend()
        {
            if (Status != EmailStatus.ReadyToSend)
                throw new EmailNotReadyBeToSentException("The email is not ready to be sent.");
        }
        private void EnsureNotRendered()
        {
            if (Status != EmailStatus.New)
                throw new EmailAlreadyRenderedException("The email is already rendered.");
        }
    }
}