using System;
using Fs.Infrastructure.ReadStorage.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.ReadStorage
{
    public static class StartupSetup
    {
        public static IServiceCollection AddReadStorage(this IServiceCollection services, Action<ReadStorageOptions> configureOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            return services
                .Configure(configureOptions)
                .AddScoped<IReadRepository, ReadRepository>()
                .AddDbContext<ReadDbContext>((serviceProvider, dbOptions) =>
                {
                    var options = serviceProvider.GetService<IOptions<ReadStorageOptions>>().Value;
                    dbOptions.UseNpgsql(options.ConnectionString);
                });
        }
    }
}
