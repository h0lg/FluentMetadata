using FluentMetadata.Builder;

namespace FluentMetadata.Specs.Builder
{
    public class DisplayBuilderTests
    {
        private readonly Metadata metadata;
        private readonly IDisplayProperty<DummyClass, string> builder;

        public DisplayBuilderTests()
        {
            metadata = new Metadata();
            builder = new DisplayBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [TestMethod]
        public void DisplayBuilder_Ctor_NullText_IsNull()
        {
            Assert.IsNull(metadata.GetNullDisplayText());
        }

        [TestMethod]
        public void DisplayBuilder_Ctor_Format_IsNull()
        {
            Assert.IsNull(metadata.GetDisplayFormat());
        }

        [TestMethod]
        public void DisplayBuilder_Ctor_Name_IsNull()
        {
            Assert.IsNull(metadata.GetDisplayName());
        }

        [TestMethod]
        public void DisplayBuilder_NullText_NullText_IsValue()
        {
            builder.NullText("TheNullText");
            Assert.AreEqual("TheNullText", metadata.GetNullDisplayText());
            Assert.IsNull(metadata.GetDisplayName());
            Assert.IsNull(metadata.GetDisplayFormat());
        }

        [TestMethod]
        public void DisplayBuilder_Name_Name_IsValue()
        {
            builder.Name("TheNameText");
            Assert.AreEqual("TheNameText", metadata.GetDisplayName());
            Assert.IsNull(metadata.GetNullDisplayText());
            Assert.IsNull(metadata.GetDisplayFormat());
        }

        [TestMethod]
        public void DisplayBuilder_Name_Function_Equals_Metadata_DisplayName()
        {
            const string displayName = "asdf";
            builder.Name(() => displayName);
            Assert.AreEqual(displayName, metadata.GetDisplayName());
            Assert.IsNull(metadata.GetNullDisplayText());
            Assert.IsNull(metadata.GetDisplayFormat());
        }

        [TestMethod]
        public void DisplayBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.AreEqual("TheFormatText", metadata.GetDisplayFormat());
            Assert.IsNull(metadata.GetNullDisplayText());
            Assert.IsNull(metadata.GetDisplayName());
        }

        [TestMethod]
        public void DisplayBuilder_Allset_IsValue()
        {
            builder.Format("TheFormatText");
            builder.Name("TheNameText");
            builder.NullText("TheNullText");
            Assert.AreEqual("TheFormatText", metadata.GetDisplayFormat());
            Assert.AreEqual("TheNameText", metadata.GetDisplayName());
            Assert.AreEqual("TheNullText", metadata.GetNullDisplayText());
        }
    }
}