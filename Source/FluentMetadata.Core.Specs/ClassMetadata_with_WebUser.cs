using FluentMetadata.Rules;
using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class ClassMetadata_with_WebUser : MetadataTestBase
    {
        private readonly Metadata classMetadata;

        public ClassMetadata_with_WebUser()
        {
            classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(WebUser));
        }

        [TestMethod]
        public void ModelName_is_Null()
        {
            Assert.IsNull(classMetadata.ModelName);
        }

        [TestMethod]
        public void ModeType_is_WebUser()
        {
            Assert.AreEqual(typeof(WebUser), classMetadata.ModelType);
        }

        [TestMethod]
        public void DisplayName_is_Benutzer()
        {
            Assert.AreEqual("Benutzer", classMetadata.GetDisplayName());
        }

        [TestMethod]
        public void Generic_name_rule_is_valid_when_Username_is_not_equal_to_AutorName()
        {
            var nameRule = classMetadata.Rules
                .OfType<GenericClassRule<WebUser>>()
                .Single();

            var webUser = new WebUser();
            webUser.Username = "Holger";
            webUser.Autor = new Autor { Name = "Albert" };

            Assert.IsTrue(nameRule.IsValid(webUser));
        }

        [TestMethod]
        public void Generic_name_rule_is_invalid_when_Username_is_equal_to_AutorName()
        {
            var nameRule = classMetadata.Rules
                .OfType<GenericClassRule<WebUser>>()
                .Single();

            var webUser = new WebUser();
            webUser.Username = "Holger";
            webUser.Autor = new Autor { Name = "Holger" };

            Console.WriteLine(nameRule.FormatErrorMessage(classMetadata.GetDisplayName()));
            Assert.IsFalse(nameRule.IsValid(webUser));
        }
    }
}