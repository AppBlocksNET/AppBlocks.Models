using AppBlocks.Config;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;
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

        public static string TestUserId = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUserId", "test@test.com");
        public static string TestUserPwd = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUserPwd", "");
    }
}