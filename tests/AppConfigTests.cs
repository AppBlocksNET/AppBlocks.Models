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
            var groupId = Factory.GetConfig().AppSettings()["AppBlocks:AppBlocks.GroupId"];
            Console.WriteLine($"AppBlocks:AppBlocks.GroupId:{groupId}");
            Assert.IsTrue(!string.IsNullOrEmpty(groupId), $"No setting:{groupId}");
        }

        [TestMethod]
        public void GroupIdSettingsTest()
        {
            Console.WriteLine($"Models.Settings.GroupId:{Models.Settings.GroupId}");
            Assert.IsTrue(Models.Settings.GroupId == "11111111-1111-1111-1111-111111111111", Models.Settings.GroupId);
        }

        [TestMethod]
        public void TestUserIdTest()
        {
            Console.WriteLine($"Settings.TestUserId:{Settings.TestUserId}");
            Assert.IsTrue(Settings.TestUserId != null);
        }

        [TestMethod]
        public void TestSettingTest()
        {
            Console.WriteLine($"Settings.TestSetting:{Settings.TestSetting}");
            Assert.IsTrue(Settings.TestSetting != null);
        }
    }
}