using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var disco = await DiscoveryClient.GetAsync("https://identityserveramardevkate.azurewebsites.net/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "clientconsole", "secretconsole");
            // var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("amar", "password", "api1");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadLine();
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            

            //Call API
            // call api
            var client = new HttpClient();
        client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadLine();

        }
    }
}
