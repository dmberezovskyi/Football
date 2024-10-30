using System;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.Storage
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, Action<StorageOptions> configureOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            return services
                .Configure(configureOptions)
                .AddScoped<IRepository, Repository>()
                .AddDbContext<AppDbContext>((serviceProvider, dbOptions) =>
                {
                    var options = serviceProvider.GetService<IOptions<StorageOptions>>().Value;
                    dbOptions.UseNpgsql(options.ConnectionString);
                });
        }
    }
}
