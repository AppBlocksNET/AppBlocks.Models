using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void ApiIdNullTest()
        {
            Console.WriteLine($"Models.Settings.ApiId:{Models.Settings.ApiId}");
            Assert.IsTrue(Models.Settings.ApiId == null);
        }

        [TestMethod]
        public void AppSettingsNotNullTest()
        {
            Console.WriteLine($"Models.Settings.AppSettings.Count:{Models.Settings.AppSettings.Count}");
            Assert.IsTrue(Models.Settings.AppSettings.Count > 0);
        }

        [TestMethod]
        public void EnvNotNullTest()
        {
            Console.WriteLine($"Models.Settings.Env:{Models.Settings.Env}");
            Assert.IsTrue(!string.IsNullOrEmpty(Models.Settings.Env));
        }

        [TestMethod]
        public void GroupIdNotNullTest()
        {
            Console.WriteLine($"Models.Settings.GroupId:{Models.Settings.GroupId}");
            Assert.IsTrue(Models.Settings.GroupId != null);
        }

        [TestMethod]
        public void GroupIdNotDefaultTest()
        {
            Console.WriteLine($"Models.Settings.GroupId:{Models.Settings.GroupId}");
            Assert.IsTrue(Models.Settings.GroupId != "5E11C4A9-D602-EB11-A38D-BC9A78563412", Models.Settings.GroupId);
        }

        [TestMethod]
        public void TestUserIdTest()
        {
            Console.WriteLine($"{this}.Settings.TestUserId:{Settings.TestUserId}");
            Assert.IsTrue(Settings.TestUserId != null);
        }

        [TestMethod]
        public void TestSettingTest()
        {
            Console.WriteLine($"{this}.Settings.TestSetting:{Settings.TestSetting}");
            Assert.IsTrue(Settings.TestSetting != null);
        }
    }
}