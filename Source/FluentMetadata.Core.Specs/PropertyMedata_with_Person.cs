using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class PropertyMedata_with_Person : MetadataTestBase
    {
        private readonly Metadata firstName, lastName;

        public PropertyMedata_with_Person()
        {
            firstName = QueryFluentMetadata.GetMetadataFor(typeof(Person), "FirstName");
            lastName = QueryFluentMetadata.GetMetadataFor(typeof(Person), "LastName");
        }

        [TestMethod]
        public void FirstName_ModelName_is_FirstName()
        {
            Assert.AreEqual("FirstName", firstName.ModelName);
        }

        [TestMethod]
        public void FirstName_ModelType_is_string()
        {
            Assert.AreEqual(typeof(string), firstName.ModelType);
        }

        [TestMethod]
        public void FirstName_Required_is_true()
        {
            Assert.IsTrue(firstName.Required.Value);
        }

        [TestMethod]
        public void LastName_ModelName_is_LastName()
        {
            Assert.AreEqual("LastName", lastName.ModelName);
        }

        [TestMethod]
        public void LastName_ModelType_is_string()
        {
            Assert.AreEqual(typeof(string), lastName.ModelType);
        }

        [TestMethod]
        public void LastName_Required_is_not_set()
        {
            Assert.IsFalse(lastName.Required.HasValue);
        }
    }
}