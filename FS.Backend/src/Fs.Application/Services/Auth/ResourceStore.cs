using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Fs.Application.Services.Auth
{
    internal class ResourceStore : IResourceStore
    {
        private static readonly IEnumerable<IdentityResource> IdentityResources;
        private static readonly IEnumerable<ApiScope> ApiScopes;
        private static readonly IEnumerable<ApiResource> ApiResources;

        static ResourceStore()
        {
            IdentityResources = new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new (JwtClaimTypes.Role, new [] {JwtClaimTypes.Role}),
                new (JwtClaimTypes.Id, new [] {JwtClaimTypes.Id})
            };
            ApiScopes = new[]
            {
                new ApiScope("api", "API")
            };
            ApiResources = new[]
            {
                new ApiResource("api")
                {
                    Name = "api",
                    DisplayName = "API",
                    Scopes = new [] { "api" },
                    UserClaims = new [] { JwtClaimTypes.Subject, JwtClaimTypes.Role, JwtClaimTypes.Id, JwtClaimTypes.Email }
                }
            };
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(IdentityResources);
        }
        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(ApiScopes);
        }
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(ApiResources);
        }
        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return Task.FromResult(ApiResources);
        }
        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources(IdentityResources, ApiResources, ApiScopes));
        }
    }
}
