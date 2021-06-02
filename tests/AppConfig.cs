using AppBlocks.Config;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
        
        public static string TestUser = Config.Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUser", "test@test.com");
        public static string TestPassword = Config.Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestPassword", "");
        public static string TestUserId = Config.Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUserId", "test@test.com");
    }
}