using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Fs.Application.Extensions
{
    internal static class IdentityServerInteractionServiceExtensions
    {
        public static async Task<(AuthorizationRequest, string)> GetAuthContextAsync(
            this IIdentityServerInteractionService service, string returnUrl)
        {
            try
            {
                var url = new Uri(returnUrl, UriKind.RelativeOrAbsolute);

                AuthorizationRequest context;
                if (url.IsAbsoluteUri)
                {
                    context = await service.GetAuthorizationContextAsync(url.PathAndQuery);
                    if (context != null)
                        returnUrl = url.PathAndQuery;
                }
                else
                {
                    if (!returnUrl.StartsWith("/"))
                        returnUrl = "/" + returnUrl;

                    context = await service.GetAuthorizationContextAsync(returnUrl);
                }

                if (context != null)
                    return (context, returnUrl);
            }
            catch
            {
                return (null, null);
            }

            return (null, null);
        }
    }
}
