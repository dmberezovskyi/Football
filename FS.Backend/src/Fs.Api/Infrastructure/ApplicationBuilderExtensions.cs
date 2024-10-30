using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Fs.Api.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestCorrections(this IApplicationBuilder builder, Uri baseUrl)
        {
            return builder.Use(async (ctx, next) =>
                {
                    ctx.Request.Scheme = baseUrl.Scheme;
                    
                    ctx.Request.Host = baseUrl.IsDefaultPort
                        ? new HostString(baseUrl.Host)
                        : new HostString(baseUrl.Host, baseUrl.Port);

                    ctx.Request.PathBase = string.IsNullOrWhiteSpace(baseUrl.AbsolutePath.Trim('/'))
                        ? new PathString()
                        : new PathString(baseUrl.AbsolutePath);

                    await next();
                }
            );
        }

        public static IApplicationBuilder UseCacheControl(this IApplicationBuilder builder)
        {
            return builder.Use(async (ctx, next) =>
                {
                    ctx.Response.OnStarting(() =>
                    {
                        if (!ctx.Response.Headers.ContainsKey("Cache-Control"))
                        {
                            ctx.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                        }

                        return Task.CompletedTask;
                    });

                    await next();
                }
            );
        }
    }
}
