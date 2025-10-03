using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    [TestClass]
    public static class GlobalTestSetup
    {
        public static Exception MetadataSetupException { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            FluentMetadataBuilder.Reset();

            try
            {
                FluentMetadataBuilder.ForAssemblyOfType<ComplexModel>();
            }
            catch (Exception ex)
            {
                MetadataSetupException = ex;
            }

            ModelMetadataProviders.Current = new FluentMetadataProvider();
        }
    }
}