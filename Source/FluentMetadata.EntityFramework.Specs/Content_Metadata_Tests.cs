using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public class Content_Metadata_Tests
    {
        [TestMethod]
        public void Content_Title_Is_Required()
        {
            var metaData = QueryFluentMetadata.GetMetadataFor(typeof(Content), "Title");
            Assert.IsTrue(metaData.Required.Value);
        }
    }
}