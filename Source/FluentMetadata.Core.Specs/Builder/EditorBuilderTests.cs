using FluentMetadata.Builder;

namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class EditorBuilderTests
    {
        private readonly Metadata metadata;
        private readonly IEditorProperty<DummyClass, string> builder;

        public EditorBuilderTests()
        {
            metadata = new Metadata();
            builder = new EditorBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [TestMethod]
        public void EditorBuilder_Ctor_ErrorMessage_IsNull()
        {
            Assert.IsNull(metadata.ErrorMessage);
        }

        [TestMethod]
        public void EditorBuilder_Ctor_Format_IsNull()
        {
            Assert.IsNull(metadata.GetEditorFormat());
        }

        [TestMethod]
        public void EditorBuilder_Ctor_Watermark_IsNull()
        {
            Assert.IsNull(metadata.GetWatermark());
        }

        [TestMethod]
        public void EditorBuilder_ErrorMessage_ErrorMessage_IsValue()
        {
            builder.ErrorMessage("TheNullText");
            Assert.AreEqual("TheNullText", metadata.ErrorMessage);
            Assert.IsNull(metadata.GetEditorFormat());
            Assert.IsNull(metadata.GetWatermark());
        }

        [TestMethod]
        public void EditorBuilder_Name_Name_IsValue()
        {
            builder.Watermark("TheNameText");
            Assert.AreEqual("TheNameText", metadata.GetWatermark());
            Assert.IsNull(metadata.GetEditorFormat());
        }

        [TestMethod]
        public void EditorBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.AreEqual("TheFormatText", metadata.GetEditorFormat());
            Assert.IsNull(metadata.GetWatermark());
        }
    }
}