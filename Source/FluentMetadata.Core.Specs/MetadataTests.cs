using FluentMetadata.Rules;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class MetadataTests
    {
        private readonly Metadata metadata;

        public MetadataTests()
        {
            metadata = new Metadata();
        }

        [TestMethod]
        public void RulesAreEmptyAndRequiredHasNoValueWhenCreatingANewInstance()
        {
            Assert.IsFalse(metadata.Required.HasValue);
            Assert.AreEqual(0, metadata.Rules.Count());
        }

        [TestMethod]
        public void SettingRequiredAddsARequiredRule()
        {
            metadata.Required = true;
            Assert.AreEqual(1, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void SettingNotRequiredAfterSettingRequiredRemovesTheRequiredAgain()
        {
            metadata.Required = true;
            metadata.Required = false;
            Assert.AreEqual(0, metadata.Rules.OfType<RequiredRule>().Count());
        }
    }
}