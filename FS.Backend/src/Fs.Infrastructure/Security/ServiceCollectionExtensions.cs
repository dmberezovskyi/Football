using System;
using Fs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fs.Infrastructure.Security
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services
                .AddScoped<IPasswordHashGenerator, PasswordHashGenerator>();
        }
    }
}
