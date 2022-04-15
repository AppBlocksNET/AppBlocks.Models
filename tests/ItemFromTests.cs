using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class ItemFromTests
    {
        private static Assembly entryAssembly;
        public static Assembly EntryAssembly => entryAssembly ?? (entryAssembly = Assembly.GetEntryAssembly()) ?? (entryAssembly = Assembly.GetExecutingAssembly());

        private static string assemblyName;
        public static string AssemblyName => assemblyName ?? (assemblyName = EntryAssembly.GetName().Name);

        [TestMethod]
        public void FromDataReaderTest()
        {
            var path = ".\\data-tests\\ItemChildrenDeserializationTest.json";
            //var folder = ApplicationData.Current.LocalFolder;
            //var file = folder.GetFileAsync(path).Result;
            //var fileStream = await file.OpenReadAsync(path);
            //var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //var inputStream = fileStream.GetInputStreamAt(0);

            //var reader = new FileDataReader(path);
            //var reader = IDataReader(fileStream);

            //var reader = new System.Windows.Storage.Streams.DataReader(input);
            //var results = new Sql().ExecuteAsync.Results
            //var item = Item.FromDataReader(".\\data-tests\\ItemChildrenDeserializationTest.json");
            //var item = Item.FromDataReader(reader);
            var item = Item.FromJson<Item>(path);
            Assert.IsTrue(item != null);
            //Assert.IsTrue(item.Children.Count() > 2);
            //item.ToFile();
        }

        [TestMethod]
        public void FromDictionaryTest()
        {
            var path = ".\\data-tests\\ItemChildrenDeserializationTest.json";
            //var folder = ApplicationData.Current.LocalFolder;
            //var file = folder.GetFileAsync(path).Result;
            //var fileStream = await file.OpenReadAsync(path);
            //var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //var inputStream = fileStream.GetInputStreamAt(0);

            //var reader = new FileDataReader(path);
            //var reader = IDataReader(fileStream);

            //var reader = new System.Windows.Storage.Streams.DataReader(input);
            //var results = new Sql().ExecuteAsync.Results
            //var item = Item.FromDataReader(".\\data-tests\\ItemChildrenDeserializationTest.json");
            //var item = Item.FromDataReader(reader);
            var item = Item.FromDictionary(new Dictionary<string, string> { ["path"] = path });
            Assert.IsTrue(item != null);
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
            var item = Item.FromJson<Item>(".\\data-tests\\ItemChildrenDeserializationTest.json");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
            item.ToFile<Item>();
        }

        [TestMethod]
        public void FromJsonFileTest()
        {
            var item = Item.FromJson<Item>(".\\data-tests\\ItemDeserializationTest.json");//?.FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Title == "test");
            //Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromJsonFileChildrenTest()
        {
            var item = Item.FromJson<Item>(".\\data-tests\\ItemChildrenDeserializationTest.json");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromJsonFileBadTest()
        {
            var item = Item.FromJson<Item>($"{AssemblyName}.CurrentUser");
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Name == $"{AssemblyName}.CurrentUser");
            //Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromJsonListFileDataTest()
        {
            var item = Item.FromJsonList<List<Item>>(".\\data-tests\\ItemChildrenDeserializationTest.json").FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
            item.ToFile<Item>();
        }

        [TestMethod]
        public void FromJsonListFileTest()
        {
            var items = Item.FromJsonList<List<Item>>(".\\data-tests\\ItemDeserializationTest.json");//?.FirstOrDefault();
            Assert.IsTrue(items != null);
            var item = items.FirstOrDefault();
            Assert.IsTrue(item.Title == "test");
        }

        [TestMethod]
        public void FromJsonListFileChildrenTest()
        {
            var item = Item.FromJsonList<List<Item>>(".\\data-tests\\ItemChildrenDeserializationTest.json")?.FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromJsonListFileBadTest()
        {
            var item = Item.FromJsonList<List<Item>>($"{AssemblyName}.CurrentUser")?.FirstOrDefault();
            Assert.IsTrue(item == null);
            //Assert.IsTrue(item.Children.Count() > 2);
        }

        [TestMethod]
        public void FromServiceListDefaultGroupTest()
        {
            var item = Item.FromServiceList<List<Item>>().FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 0);
        }

        [TestMethod]
        public void FromServiceTest()
        {
            var item = Item.FromService<Item>(Context.GroupId); // Item.FromService<Item>(Models.Settings.GroupId);
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 0);
        }

        [TestMethod]
        public void FromServiceListCleanTest()
        {
            if (System.IO.File.Exists(Common.GetFilepath(Context.GroupId))) System.IO.File.Delete(Common.GetFilepath(Context.GroupId));
            Assert.IsFalse(System.IO.File.Exists(Common.GetFilepath(Context.GroupId)));
            var item = Item.FromServiceList<List<Item>>().FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Children.Count() > 0);
        }
        [TestMethod]
        public void FromXmlFileTest()
        {
            var item = Item.FromXml<Item>(".\\data-tests\\item.test.xml");//?.FirstOrDefault();
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Title == "test");
        }

        //[TestMethod]
        //public void FromXmlListFileTest()
        //{
        //    var items = Item.FromXmlList<List<Item>>(".\\data-tests\\item.test.xml");//?.FirstOrDefault();
        //    Assert.IsTrue(items != null);
        //    var item = items.FirstOrDefault();
        //    Assert.IsTrue(item.Title == "test");
        //}
    }
}