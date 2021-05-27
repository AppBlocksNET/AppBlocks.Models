using AppBlocks.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AppBlocks.Models.Tests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public void DbContextTest()
        {
            using (DbContext dbContext = Factory.CreateDbContext())
            {
                Assert.IsNotNull(dbContext, "no one's home");
            }            
        }


        [TestMethod]
        public void HomeTest()
        {
            Item item = null;
            using (DbContext dbContext = Factory.CreateDbContext())
            {
                item = dbContext?.Items?.FirstOrDefault(i => i.Name == "Home");
            }
            Assert.IsNotNull(item, "no one's home");
        }
    }
}