using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class ClassMetadata_with_WebUserIndexModel : MetadataTestBase
    {
        private readonly Metadata classMetadata;
        public ClassMetadata_with_WebUserIndexModel() { classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexModel)); }
        [TestMethod] public void ModelName_is_Null() { Assert.IsNull(classMetadata.ModelName); }
        [TestMethod] public void ModelType_is_WebUserIndexModel() { Assert.AreEqual(typeof(WebUserIndexModel), classMetadata.ModelType); }
        [TestMethod] public void DisplayName_is_Benutzer() { Assert.AreEqual("Benutzer", classMetadata.GetDisplayName()); }
    }

    [TestClass]
    public class ClassMetadata_with_WebUserIndexGetModel : MetadataTestBase
    {
        private readonly Metadata classMetadata;
        public ClassMetadata_with_WebUserIndexGetModel() { classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel)); }
        [TestMethod] public void DisplayName_is_Benutzer() { Assert.AreEqual("Benutzer", classMetadata.GetDisplayName()); }
    }
}