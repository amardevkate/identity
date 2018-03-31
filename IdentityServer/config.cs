using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class config
    {
        public static IConfiguration _configuration;
        private const string V = "api1";

        //public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource> {
        //          new ApiResource(V, "My API")
        //};

        public static IEnumerable<ApiResource> GetApiResources()
        {
            var ApiResourcelist = new List<ApiResource>();

            List<string> api = _configuration.GetSection("ApiCollection:API").Get<List<string>>();
            foreach (var item in api)
            {
                ApiResourcelist.Add(new ApiResource(item));
            }
            return ApiResourcelist;
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = _configuration["ClientId"].ToString(),

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                
                    // secret for authentication
                    ClientSecrets = {new Secret(_configuration["Secret"].ToString().Sha256())},

                    // scopes that client has access to
                    AllowedScopes = { V }
                }
            };
        }
    }
}
