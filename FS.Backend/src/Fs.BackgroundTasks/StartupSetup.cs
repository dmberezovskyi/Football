using Fs.BackgroundTasks.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fs.BackgroundTasks
{
    public static class StartupSetup
    {
        public static IServiceCollection AddABackgroundTasks(this IServiceCollection services)
        {
            return services
                .AddHostedService<EmailRenderService>()
                .AddHostedService<EmailSenderService>();
        }
    }
}
