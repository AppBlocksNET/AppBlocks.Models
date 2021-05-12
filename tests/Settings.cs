using AppBlocks.Config;
using System.Collections.Generic;

namespace AppBlocks.Models.Tests
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        public static string TestUserId = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUserId", "test@test.com");
        public static string TestUserPwd = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestUserPwd", "");
        public static string TestSetting = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestSetting", "");
    }
}