using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace AppBlocks.Models.Tests
{
    public static class AppConfig
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            return config;
        }
    }
}