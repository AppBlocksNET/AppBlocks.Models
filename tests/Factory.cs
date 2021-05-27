using Microsoft.EntityFrameworkCore;

namespace AppBlocks.Models.Tests
{
    public static class Factory
    {
        public static DbContext CreateDbContext(string connectionStringId = "AppBlocks")
        {
            return CreateDbContext(new[] { connectionStringId });
        }

        public static DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            return new DbContext(optionsBuilder.Options);
        }
    }
}