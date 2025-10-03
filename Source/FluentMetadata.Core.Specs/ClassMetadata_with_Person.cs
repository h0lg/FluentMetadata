using FluentMetadata.Rules;
using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class ClassMetadata_with_Person : MetadataTestBase
    {
        private readonly Metadata classMetadata;

        public ClassMetadata_with_Person()
        {
            classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(Person));
        }

        [TestMethod]
        public void Metadata_ModelType_is_Person()
        {
            Assert.AreEqual(typeof(Person), classMetadata.ModelType);
        }

        [TestMethod]
        public void Metadata_ModelName_is_Null()
        {
            Assert.IsNull(classMetadata.ModelName);
        }

        [TestMethod]
        public void Metadata_Display_is_Benutzer()
        {
            Assert.AreEqual("Benutzer", classMetadata.GetDisplayName());
        }

        [TestMethod]
        public void Instance_with_FirstName_different_from_LastName_is_invalid()
        {
            var rule = classMetadata.Rules.OfType<PropertyMustMatchRule<Person>>().Last();
            var person = new Person { FirstName = "foo", LastName = "bar" };
            Assert.IsFalse(rule.IsValid(person));
        }

        [TestMethod]
        public void Instance_with_FirstName_equal_to_LastName_is_valid()
        {
            var rule = classMetadata.Rules.OfType<PropertyMustMatchRule<Person>>().Last();
            var person = new Person { FirstName = "foo", LastName = "foo" };
            Assert.IsTrue(rule.IsValid(person));
        }
    }
}