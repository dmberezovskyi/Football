using Fs.Api.Infrastructure.Error;
using Fs.Api.Infrastructure.Filters;
using Fs.Api.Infrastructure.Services;
using Fs.Application.Extensions;
using Fs.Application.Services.Auth;
using Fs.Infrastructure.Auth.Abstractions;
using Fs.Infrastructure.EmailServices.Smtp;
using Fs.Infrastructure.ReadStorage;
using Fs.Infrastructure.Security;
using Fs.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Fs.Api.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            return services
                .AddScoped<IErrorResponseBuilder, ErrorResponseBuilder>()
                .AddScoped<CommonExceptionFilterAttribute>();
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            return services
                .AddStorage(options =>
                {
                    options.ConnectionString = connectionString;
                })
                .AddReadStorage(options =>
                {
                    options.ConnectionString = connectionString;
                })
                .AddSmtpService(options =>
                {
                    options.Host = "smtp.example.com";
                    options.Port = 587;
                    options.UseSsl = true;
                    options.UserName = "fs@fs.com";
                    options.Password = "secretpassword";
                    options.From = "fs@fs.com";
                })
                .AddSecurityServices()
                .AddScoped<IUserContextService, UserContextService>();
        }
        public static IServiceCollection AddApplication(this IServiceCollection services, string origin)
        {
            return services
                .AddMediatRServices()
                .AddPolicyServices()
                .AddAppServices()
                .AddAuth(options =>
                {
                    options.Origin = origin;
                    options.IssuerUri = $"http://localhost:5001";
                    options.BaseUrl = $"{origin}";
                    options.LoginUrl = $"{origin}/auth/login";
                    options.LogoutUrl = $"{origin}/auth/logout";
                })
                .ConfigureFrontEndOptions(options =>
                {
                    options.BaseUrl = $"{origin}";
                    options.EmailConfirmationUrl = origin + "/email-confirmation/{0}";
                });
        }
    }
}
