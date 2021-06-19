using AppBlocks.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class AppConfigTests
    {
        [TestMethod]
        public void GroupIdTest()
        {
            var groupId = Config.Factory.GetConfig().AppSettings()["AppBlocks:AppBlocks.GroupId"];
            Console.WriteLine($"AppBlocks:AppBlocks.GroupId:{groupId}");
            Assert.IsTrue(!string.IsNullOrEmpty(groupId), $"No setting:{groupId}");
        }

        [TestMethod]
        public void GroupIdSettingsTest()
        {
            Console.WriteLine($"Models.Settings.GroupId:{Context.GroupId}");
            Assert.IsTrue(Context.GroupId == "AppBlocks.Core.Test.App", Context.GroupId);
        }
    }
}