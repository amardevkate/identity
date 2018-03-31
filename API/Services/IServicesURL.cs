using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IServicesURL
    {
        string GetIdentityServiceURL();
    }

    public class ServicesURL:IServicesURL
    {
        private IConfiguration _configuration;

        public ServicesURL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string IServicesURL.GetIdentityServiceURL()
        {
            return _configuration["ServiceURL:IdentityURL"];
        }
    }
}
