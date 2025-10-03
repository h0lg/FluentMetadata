using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    [TestClass]
    public class Required_rule_specs
    {
        private readonly RequiredRule Sut;

        public Required_rule_specs()
        {
            Sut = new RequiredRule();
        }

        [TestMethod]
        public void Null_is_not_valid()
        {
            Assert.IsFalse(Sut.IsValid(null));
        }

        [TestMethod]
        public void Empty_string_is_not_valid()
        {
            Assert.IsFalse(Sut.IsValid(""));
        }

        [TestMethod]
        public void AnObject_is_Valid()
        {
            Assert.IsTrue(Sut.IsValid(DateTime.Now));
        }

        [TestMethod]
        public void AString_is_Valid()
        {
            Assert.IsTrue(Sut.IsValid("hallo"));
        }

        [TestMethod]
        public void Number_99_is_Valid()
        {
            Assert.IsTrue(Sut.IsValid(99));
        }

        [TestMethod]
        public void Number_99_1_is_Valid()
        {
            Assert.IsTrue(Sut.IsValid(99.1));
        }
    }
}