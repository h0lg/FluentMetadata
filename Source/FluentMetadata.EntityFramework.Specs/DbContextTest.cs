using System.Data.Entity;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public class DbContextTest
    {
        public DbContextTest()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RegularlyDbContext>());
        }

        [TestMethod]
        public void CanCreateDbContext()
        {
            if (File.Exists("TestDatabase.sdf"))
            {
                File.Delete("TestDatabase.sdf");
            }
            var context = new RegularlyDbContext();
        }
    }

    public class NoDatabaseCreate<T> : IDatabaseInitializer<T> where T : DbContext
    {
        public void InitializeDatabase(T context)
        {
        }
    }
}