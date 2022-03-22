using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void ItemDeserializationTest()
        {
            var path = $".\\data-tests\\AppBlocksApiAllTests.json";
            var content = System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : "";
            Assert.IsTrue(!string.IsNullOrEmpty(content), $"{path} not found");

            //var value = content;
            var results = System.Text.Json.JsonSerializer.DeserializeAsync<List<Item>>(new MemoryStream(Encoding.UTF8.GetBytes(content))).Result;

            Assert.IsTrue(results != null);

            Assert.IsTrue(results.Count > 2);
        }

        [TestMethod]
        public void ItemChildrenTest()
        {
            var item = new Item($".\\data-tests\\ItemChildrenDeserializationTest.json");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children != null && item.Children.Count() > 0, $"{item?.Id}:{item?.Children?.Count()}");
        }

        [TestMethod]
        public void ItemChildrenDeserializationTest()
        {
            var path = $".\\data-tests\\ItemChildrenDeserializationTest.json";
            var content = System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : "";
            Assert.IsTrue(!string.IsNullOrEmpty(content), $"{path} not found");

            //var value = content;
            var results = System.Text.Json.JsonSerializer.DeserializeAsync<List<Item>>(new MemoryStream(Encoding.UTF8.GetBytes(content))).Result;
            
            //var group = new Item(content);
            Assert.IsTrue(results != null);

            var item = new Item(results);

            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children != null && item.Children.Count() > 0, $"{item?.Id}:{item?.Children?.Count()}");
        }

        [TestMethod]
        public void ItemListWithoutGroupConstructorTest()
        {
            var items = new List<Item> { 
                new Item() { Id = "Test Child 1", FullPath = "/Test/Test Child 1", Name = "Test Child 1", Path = "Test Child 1",ParentId = "Test", TypeId = "" },
                new Item() { Id = "Test", FullPath = "/Test", Name = "Test", Path = "Test", TypeId = "" },
                new Item() { Id = "Test Child 2", FullPath = "/Test/Test/Test Child 2", Name = "Test Child 2", Path = "Test Child 2",ParentId = "Test", TypeId = "" },
                new Item() { Id = "Test Grandchild 1", FullPath = "/Test/Test Child 1/Test Grandchild 1", Name = "Test Grandchild 1", Path = "Test Grandchild 1",ParentId = "Test Child 1", TypeId = "" },
            };
            var group = new Item(items);

            Assert.IsTrue(group != null && group.Id == "Test Child 1", $"{group.Id}");

            Assert.IsTrue(group != null && group.Children != null && group.Children.Count() > 0, $"{group?.Id}:{group?.Children?.Count()}");
        }

        [TestMethod]
        public void ItemListWithGroupConstructorTest()
        {
            var items = new List<Item> {
                new Item() { Id = "Test Child 1", Name = "Test", Path = "Test",ParentId = "Test", TypeId = "" },
                new Item() { Id = "Test", Name = "Test", Path = "Test", TypeId = "78618B97-39FF-EA11-A38B-BC9A78563412" },
                new Item() { Id = "Test Child 2", Name = "Test", Path = "Test",ParentId = "Test", TypeId = "" },
            };
            var group = new Item(items);

            Assert.IsTrue(group != null && group.Id == "Test");

            Assert.IsTrue(group.Children != null);

            Assert.IsTrue(group != null && group.Children != null && group.Children.Count() > 0);
        }

        [TestMethod]
        public void ItemHttpTest()
        {
            var group = Context.GroupId;
            Assert.IsTrue(group != null && group != "Test");
            var item = new Item(new System.Uri($"{Context.AppBlocksBlocksServiceUrl}{group}"));

            Assert.IsTrue(item.Children != null);

            Assert.IsTrue(item != null && item.Children != null && item.Children.Count() > 0);

            var write = item.ToFile<Item>();
            Assert.IsTrue(write);
        }

        [TestMethod]
        public void FromFileTest()
        {//TODO: read the data\itemtest.json file - a known filename\location
            var testItem = new Item("test");
            Assert.IsTrue(testItem != null);
            Assert.IsTrue(testItem.Title == "test");
            var write = testItem.ToFile<Item>();
            Assert.IsTrue(write);
            Assert.IsTrue(System.IO.File.Exists(testItem.GetFilename()));

            //var xml = testItem.ToXml();
            write = testItem.ToFile<Item>(schema:"xml");
            Assert.IsTrue(write);
            var filePath = testItem.GetFilename(schema:"xml");
            Assert.IsTrue(System.IO.File.Exists(filePath));
        }

        //[TestMethod]
        //public void ItemSettingsTest()
        //{
        //    var testItem = new Item("test");
        //    Assert.IsNotNull(testItem);
        //    var setting = testItem.GetSetting<string>("Test", "1");
        //    Assert.IsTrue(setting == "1");
        //}

        //[TestMethod]
        //public void ItemSettingsText()
        //{
        //    var testItem = new Item("test");
        //    Assert.IsNotNull(testItem);
        //    var setting = testItem.GetSetting<string>("color", "blue");
        //    Assert.IsTrue(setting == "blue");
        //}

        //[TestMethod]
        //public void ItemSettingsBoolTrue()
        //{
        //    var testItem = new Item("test");
        //    Assert.IsNotNull(testItem);
        //    var setting = testItem.GetSetting<bool>("favorite", "true");
        //    Assert.IsTrue(setting);
        //}

        //[TestMethod]
        //public void ItemSettingsBoolFalse()
        //{
        //    var testItem = new Item("test");
        //    Assert.IsNotNull(testItem);
        //    var setting = testItem.GetSetting<bool>("favorite", "false");
        //    Assert.IsTrue(!setting);
        //}

        [TestMethod]
        public void ToFromFileTest()
        {
            var testItem = new Item() { Id="test",Name="test",Title="test"};
            var write = testItem.ToFile<Item>();
            Assert.IsTrue(write);
            Assert.IsTrue(System.IO.File.Exists(testItem.GetFilename()));
        }

        [TestMethod]
        public void WriteChildrenTest()
        {
            var testItem = new Item()
            {
                Id = "children",
                Name = "children",
                Title = "children",
                Children = new List<Item>() {
                    new Item() { Id = "child1", Name = "child1", Title = "child1" },
                    new Item() { Id = "child2", Name = "child2", Title = "child2" }}
            };
            var write = testItem.ToFile<Item>();
            Assert.IsTrue(write);
            Assert.IsTrue(System.IO.File.Exists(testItem.GetFilename()));
        }
    }
}