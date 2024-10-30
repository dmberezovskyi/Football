using System;
using Fs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fs.Infrastructure.EmailServices.Smtp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmtpService(this IServiceCollection services, Action<SmtpOptions> configureOptions)
        {
            
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            return services
                .Configure(configureOptions)
                .AddScoped<IEmailService, SmtpService>();
        }
    }
}
