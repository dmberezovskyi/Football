using System;
using FluentValidation;
using Fs.Application.Behaviors;
using Fs.Application.Behaviors.Command;
using Fs.Application.Behaviors.Common;
using Fs.Application.Behaviors.Policy;
using Fs.Application.Permissions;
using Fs.Application.Permissions.Abstractions;
using Fs.Application.Permissions.Pdp;
using Fs.Application.Permissions.User.Pdp;
using Fs.Application.Services;
using Fs.Application.Services.HtmlRender;
using Fs.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fs.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPolicyServices(this IServiceCollection services)
        {
            return services
                .AddScoped<UserPolicyDecisionPoint>()
                .AddScoped<IPolicyInformationPoint, PolicyInformationPoint>()
                .AddScoped<TeamPolicyDecisionPoint>();
        }
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            services
                .AddMediatR(assembly)
                .AddValidatorsFromAssembly(assembly);

            // Add common pipeline behavior
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
            // Add command pipeline behavior
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DbContextBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventsBehaviour<,>));

            // Add policy pipeline behavior
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(PermissionsContextBehaviour<,>));

            return services;
        }
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITeamService, TeamService>()
                .AddScoped<IOrganizationService, OrganizationService>()
                .AddScoped<IJsonSerializer, JsonSerializer>()
                .AddScoped<IHtmlRenderService, HtmlRenderService>();
        }
        public static IServiceCollection ConfigureFrontEndOptions(this IServiceCollection services, Action<FrontEndOptions> configureOptions)
        {
            return services
                .Configure(configureOptions);
        }
    }
}
