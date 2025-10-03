using FluentMetadata.Builder;

namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class ShouldBuilderTests
    {
        private readonly IShouldProperty<DummyClass, string> shouldBuilder;
        private readonly Metadata metadata;

        public ShouldBuilderTests()
        {
            metadata = new Metadata();
            shouldBuilder = new ShouldBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [TestMethod]
        public void ShouldBuilder_Ctor_ShouldShowDisplay()
        {
            Assert.IsTrue(metadata.ShowDisplay);
        }

        [TestMethod]
        public void ShouldBuilder_ShowInDisplay_ShouldShowDisplay()
        {
            shouldBuilder.ShowInDisplay();
            Assert.IsTrue(metadata.ShowDisplay);
        }

        [TestMethod]
        public void ShouldBuilder_Not_ShowInDisplay_ShouldNotShowDisplay()
        {
            shouldBuilder.Not.ShowInDisplay();
            Assert.IsFalse(metadata.ShowDisplay);
        }

        [TestMethod]
        public void ShouldBuilder_Ctor_ShouldShowEditor()
        {
            Assert.IsTrue(metadata.ShowEditor);
        }

        [TestMethod]
        public void ShouldBuilder_ShowInEditor_ShouldShowEditor()
        {
            shouldBuilder.ShowInEditor();
            Assert.IsTrue(metadata.ShowEditor);
        }

        [TestMethod]
        public void ShouldBuilder_Not_ShowInEditor_ShouldNotShowEditor()
        {
            shouldBuilder.Not.ShowInEditor();
            Assert.IsFalse(metadata.ShowEditor);
        }

        [TestMethod]
        public void ShouldBuilder_Ctor_ShouldNotHideSurroundingHtml()
        {
            Assert.IsFalse(metadata.HideSurroundingHtml.HasValue);
        }

        [TestMethod]
        public void ShouldBuilder_HideSurroundingHtml_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HideSurroundingHtml();
            Assert.IsTrue(metadata.HideSurroundingHtml.Value);
        }

        [TestMethod]
        public void ShouldBuilder_Not_HideSurroundingHtml_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HideSurroundingHtml();
            Assert.IsFalse(metadata.HideSurroundingHtml.Value);
        }

        [TestMethod]
        public void ShouldBuilder_Ctor_ShouldNotHiddenInput()
        {
            Assert.IsFalse(metadata.Hidden.HasValue);
        }

        [TestMethod]
        public void ShouldBuilder_HiddenInput__ShouldHiddenInput()
        {
            shouldBuilder.HiddenInput();
            Assert.IsTrue(metadata.Hidden.Value);
        }

        [TestMethod]
        public void ShouldBuilder_HiddenInput_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HiddenInput();
            Assert.IsTrue(metadata.HideSurroundingHtml.Value);
        }

        [TestMethod]
        public void ShouldBuilder_Not_HiddenInput__ShouldNotHiddenInput()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.IsFalse(metadata.Hidden.Value);
        }

        [TestMethod]
        public void ShouldBuilder_Not_iddenInput_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.IsFalse(metadata.HideSurroundingHtml.Value);
        }
    }
}