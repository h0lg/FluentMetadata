using FluentMetadata.Builder;
using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Builder
{

    [TestClass]
    public class IsBuilderTests
    {
        private readonly IIsProperty<DummyClass, string> isBuilder;
        private readonly Metadata metadata;

        public IsBuilderTests()
        {
            metadata = new Metadata();
            isBuilder = new IsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [TestMethod]
        public void IsBuilder_Ctor_IsNotSet()
        {
            Assert.IsFalse(metadata.Required.HasValue);
        }

        [TestMethod]
        public void IsBuilder_Ctor_IsNotReadOnly()
        {
            Assert.IsFalse(metadata.ReadOnly);
        }

        [TestMethod]
        public void SettingRequiredResultsInMetadataRequiredAnd1RequiredRule()
        {
            isBuilder.Required();
            Assert.IsTrue(metadata.Required.Value);
            Assert.AreEqual(1, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void SettingNotRequiredResultsInMetadataNotRequiredAnd0RequiredRules()
        {
            isBuilder.Not.Required();
            Assert.IsFalse(metadata.Required.Value);
            Assert.AreEqual(0, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void SettingNotRequiredAfterRequiredResultsInMetadataNotRequiredAnd0RequiredRules()
        {
            isBuilder.Required();
            isBuilder.Not.Required();
            Assert.IsFalse(metadata.Required.Value);
            Assert.AreEqual(0, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void IsBuilder_Readonly_IsReadOnly()
        {
            isBuilder.ReadOnly();
            Assert.IsTrue(metadata.ReadOnly);
        }

        [TestMethod]
        public void IsBuilder_Not_Readonly_IsNotReadOnly()
        {
            isBuilder.Not.ReadOnly();
            Assert.IsFalse(metadata.ReadOnly);
        }
    }
}