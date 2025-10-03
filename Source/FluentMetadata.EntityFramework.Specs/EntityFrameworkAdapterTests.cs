using System.Data.Entity;
using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    //TODO reinclude tests after updating to newest version of Entity Framework
    [TestClass]
    public class EntityFrameworkAdapterTests
    {
        private readonly DbModelBuilder modelBuilder;
        private readonly EntityFrameworkAdapter adapter;

        public EntityFrameworkAdapterTests()
        {
            modelBuilder = new DbModelBuilder();
            adapter = new EntityFrameworkAdapter();
        }

        [TestMethod, Ignore("This test was written against an outdated version of Entity Framework")]
        public void Can_Map_WebUser()
        {
            adapter.MapProperties(modelBuilder.Entity<WebUser>());
        }

        [TestMethod, Ignore("This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Content()
        {
            adapter.MapProperties(modelBuilder.Entity<Content>());
        }

        [TestMethod, Ignore("This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Layout()
        {
            adapter.MapProperties(modelBuilder.Entity<Layout>());
        }
    }
}