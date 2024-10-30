using System;
using System.Collections.Generic;
using System.IO;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Fs.Application.Services.Auth
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, Action<AuthOptions> configureOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            services
                .Configure(configureOptions);

            var authOptions = new AuthOptions();
            configureOptions(authOptions);

            var idsrvBuilder = services
                .AddIdentityServer(options =>
                {
                    options.IssuerUri = authOptions.IssuerUri;
                    options.UserInteraction.LoginUrl = authOptions.LoginUrl;
                    options.UserInteraction.LogoutUrl = authOptions.LogoutUrl;
                    options.Authentication.CookieLifetime = TimeSpan.FromDays(7);
                    options.Authentication.CookieSlidingExpiration = true;
                    options.UserInteraction.LoginReturnUrlParameter = "ReturnUrl";
                })
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>();
            
            services.AddSingleton<ICorsPolicyService>((container) => {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                
                return new DefaultCorsPolicyService(logger) {
                    AllowedOrigins = { "http://localhost:3000" }
                };
            });

            using var sr = new StreamReader(assembly.GetManifestResourceStream("Fs.Application.Services.Auth.web.jwk")
                                            ?? throw new InvalidOperationException("Json web key not found"));
            var jwk = new JsonWebKey(sr.ReadToEnd());
            idsrvBuilder.AddSigningCredential(jwk, jwk.Alg);

            services
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = authOptions.IssuerUri;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        IssuerSigningKey = jwk
                    };
                });

            return services;
        }
    }
}
