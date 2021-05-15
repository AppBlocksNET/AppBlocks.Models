using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AuthenticateTest()
        {
            var results = User.Authenticate(Settings.TestUserId, Settings.TestUserPwd);
            Console.WriteLine($"Settings.TestUserId:{Settings.TestUserId}");
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void AuthenticateBadUserTest()
        {
            var results = User.Authenticate("bad", "user");
            Assert.IsFalse(results);
        }

        [TestMethod]
        public void CurrentUserKeyTest()
        {
            var results = new User(Settings.TestUserId);
            Console.WriteLine($"Settings.TestUserId:{Settings.TestUserId}");
            Assert.IsTrue(results != null);
        }


        [TestMethod]
        public void UserDeserializationTest()
        {
            var path = $".\\data-tests\\UserDeserializationTest.json";
            var content = System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : "";
            Assert.IsTrue(!string.IsNullOrEmpty(content), $"{path} not found");

            //var value = content;
            var results = System.Text.Json.JsonSerializer.DeserializeAsync<List<User>>(new MemoryStream(Encoding.UTF8.GetBytes(content))).Result;

            Assert.IsTrue(results != null);

            Assert.IsTrue(results.Count > 2);
        }

        //[TestMethod]
        //public void ItemHttpTest()
        //{
        //    var item = new Item(new System.Uri($"{AppSettings.AppBlocksBlocksServiceUrl}{AppSettings.GroupId}"));

        //    //Assert.IsTrue(group != null && group.Id == "Test");

        //    Assert.IsTrue(item.Children != null);

        //    Assert.IsTrue(item != null && item.Children != null && item.Children.Count() > 0);

        //    var write = item.Write();
        //    Assert.IsTrue(write);
        //}

        [TestMethod]
        public void FileTest()
        {//TODO: read the data\itemtest.json file - a known filename\location
            var testItem = User.FromJson<User>(Settings.TestUserId);
            Assert.IsTrue(testItem != null);
            var write = testItem.ToFile<User>();
            Assert.IsTrue(write);
            Assert.IsTrue(System.IO.File.Exists(testItem.GetFilename("User")));
        }

        [TestMethod]
        public void WriteReadTest()
        {
            var item = new User() { Id = Settings.TestUserId, Name = Settings.TestUserId, Title = Settings.TestUserId, Password = Settings.TestUserId, Email = Settings.TestUserId };
            var write = item.ToFile<User>();
            Assert.IsTrue(write);
            Assert.IsTrue(System.IO.File.Exists(item.GetFilename("User")));
        }
    }
}