using FluentMetadata.Builder;

namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class AsBuilderTests
    {
        private readonly Metadata metadata;
        private readonly IAsProperty<DummyClass, string> asBuilder;

        public AsBuilderTests()
        {
            metadata = new Metadata();
            asBuilder = new AsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [TestMethod]
        public void AsBuilder_Ctor_DataTypeName_IsNull()
        {
            Assert.IsNull(metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_EmailAdress_DataTypeName_is_EmailAdress()
        {
            asBuilder.EmailAddress();
            Assert.AreEqual("EmailAddress", metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_Url_DataTypeName_is_Url()
        {
            asBuilder.Url();
            Assert.AreEqual("Url", metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_Html_DataTypeName_is_Html()
        {
            asBuilder.Html();
            Assert.AreEqual("Html", metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_Text_DataTypeName_is_Text()
        {
            asBuilder.Text();
            Assert.AreEqual("Text", metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_MultilineText_DataTypeName_is_MultilineText()
        {
            asBuilder.MultilineText();
            Assert.AreEqual("MultilineText", metadata.DataTypeName);
        }

        [TestMethod]
        public void AsBuilder_Password_DataTypeName_is_Password()
        {
            asBuilder.Password();
            Assert.AreEqual("Password", metadata.DataTypeName);
        }
    }
}