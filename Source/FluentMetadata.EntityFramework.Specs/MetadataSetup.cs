using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public static class GlobalTestSetup
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<WebUser>();
        }
    }
}