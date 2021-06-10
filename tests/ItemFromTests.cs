using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class ItemFromTests
    {
        private static Assembly entryAssembly;
        public static Assembly EntryAssembly => entryAssembly ?? (entryAssembly = Assembly.GetEntryAssembly());

        private static string assemblyName;
        public static string AssemblyName => assemblyName ?? (assemblyName = EntryAssembly.GetName().Name);

        [TestMethod]
        public void FromDataReaderTest()
        {
            //var item = Item.FromDataReader(".\\data-tests\\ItemChildrenDeserializationTest.json");
            //Assert.IsTrue(item != null);
            //Assert.IsTrue(item.Children.Count() > 2);
            //item.ToFile();
        }

        [TestMethod]
        public void FromEnumerableReaderTest()
        {
            //var item = Item.FromDataReader(".\\data-tests\\ItemChildrenDeserializationTest.json");
            //Assert.IsTrue(item != null);
            //Assert.IsTrue(item.Children.Count() > 2);
            //item.ToFile();
        }

        [TestMethod]
        public void FromJsonFileDataTest()
        {
            var item = new Item(".\\data-tests\\ItemChildrenDeserializationTest.json");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
            item.ToFile<Item>();
        }

        [TestMethod]
        public void FromJsonFileTest()
        {
            var item = new Item().FromJson<Item>("test");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Title == "test");
        }

        [TestMethod]
        public void FromJsonFileChildrenTest()
        {
            var item = new Item(".\\data-tests\\ItemChildrenDeserializationTest.json");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromJsonFileBadTest()
        {
            var item = new Item($"{AssemblyName}.CurrentUser");
            Assert.IsTrue(item == null);
            //Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromServiceDefaultGroupTest()
        {
            var item = new Item().FromService<Item>();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromServiceTest()
        {
            var item = new Item(Settings.GroupId); // Item.FromService<Item>(Models.Settings.GroupId);
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromServiceCleanTest()
        {
            if (System.IO.File.Exists(Common.GetFilepath(Models.Settings.GroupId))) System.IO.File.Delete(Common.GetFilepath(Models.Settings.GroupId));
            Assert.IsFalse(System.IO.File.Exists(Common.GetFilepath(Models.Settings.GroupId)));
            var item = new Item().FromService<Item>();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }
    }
}