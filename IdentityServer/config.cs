using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
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
                   // AllowedGrantTypes = GrantTypes.ClientCredentials,
                 AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    // secret for authentication
                    ClientSecrets = {new Secret(_configuration["Secret"].ToString().Sha256())},
                    // scopes that client has access to
                    AllowedScopes = { V }
                },
                new Client {              ClientId = "mvc",
            ClientName = "MVC Client",
            AllowedGrantTypes = GrantTypes.HybridAndClientCredentials ,
            ClientSecrets={new Secret (_configuration["Secret"].ToString().Sha256 ()) },

            // where to redirect to after login
            RedirectUris = { "http://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "api1"
            },
            AllowOfflineAccess =true

                }
            };
        }

        /// <summary>
        /// Functions return Test User that will return test users.
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "amar",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "varsha",
                    Password = "password"
                }

            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
        }

    }
}
