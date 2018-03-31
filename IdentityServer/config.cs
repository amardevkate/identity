using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class config
    {
        private const string V = "api1";

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource> {
            new ApiResource(V, "My API")
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = {new Secret("secret".Sha256())},

                    // scopes that client has access to
                    AllowedScopes = { V }
                }
            };
    }
}
