using System;
using Microsoft.Extensions.Configuration;

namespace Fs.Api.Infrastructure
{
    internal class AppConfiguration
    {
        private readonly IConfiguration _configuration;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Origin => _configuration["Origin"];
        public string DatabaseConnectionString => _configuration.GetConnectionString("PostgresConnectionString");

        private static string Get(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
