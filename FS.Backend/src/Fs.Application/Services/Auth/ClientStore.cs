using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Options;

namespace Fs.Application.Services.Auth
{
    internal class ClientStore : IClientStore
    {
        private readonly Client[] _clients;

        public ClientStore(IOptions<AuthOptions> options)
        {
            _clients = new[]
            {
                new Client
                {
                    ClientId  = "development",
                    ClientName = "The development client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ECAACF65-3D57-46E9-BF3D-BE1F2B154E04".Sha256()) },
                    AllowedScopes = { "api" },
                    AccessTokenLifetime = 36000
                },
                new Client
                {
                    ClientId  = "web",
                    ClientName = "The web client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = false,
                    RedirectUris = { $"{options.Value.BaseUrl}/auth/callback" },
                    PostLogoutRedirectUris = { $"{options.Value.BaseUrl}/auth/logout" },
                    AccessTokenLifetime = 36000,
                    AllowedScopes =
                    {
                        "openid", "profile", "api"
                    }
                }
            };
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(_clients.First(x => x.ClientId == clientId));
        }
    }
}
