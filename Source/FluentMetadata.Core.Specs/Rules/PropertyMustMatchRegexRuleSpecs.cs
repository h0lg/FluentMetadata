using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    [TestClass]
    public class When_property_value_should_match_a_regex
    {
        private readonly PropertyMustMatchRegexRule Sut;

        public When_property_value_should_match_a_regex()
        {
            //from http://regexlib.com/REDetails.aspx?regexp_id=96
            const string validUri = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            Sut = new PropertyMustMatchRegexRule(validUri);
        }

        [TestMethod]
        public void A_null_value_is_valid() // because to check this is the responsibility of the RequiredRule
        {
            Assert.IsTrue(Sut.IsValid(null));
        }

        [TestMethod]
        public void A_value_matching_the_pattern_is_valid()
        {
            Assert.IsTrue(Sut.IsValid("http://regexlib.com/REDetails.aspx?regexp_id=96"));
        }

        [TestMethod]
        public void A_value_not_matching_the_pattern_is_invalid()
        {
            Assert.IsFalse(Sut.IsValid("regexlib.com/REDetails.aspx?regexp_id=96"));
        }

        [TestMethod]
        public void An_empty_string_value_is_valid() // because to check this is not the responsibility of the PropertyMustMatchRegexRule
        {
            Assert.IsTrue(Sut.IsValid(string.Empty));
        }
    }

    [TestClass]
    public class When_property_value_should_not_match_a_regex
    {
        private readonly PropertyMustNotMatchRegexRule Sut;

        public When_property_value_should_not_match_a_regex()
        {
            //from http://regexlib.com/REDetails.aspx?regexp_id=96
            const string validUri = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            Sut = new PropertyMustNotMatchRegexRule(validUri);
        }

        [TestMethod]
        public void A_null_value_is_valid() // because to check this is the responsibility of the RequiredRule
        {
            Assert.IsTrue(Sut.IsValid(null));
        }

        [TestMethod]
        public void A_value_matching_the_pattern_is_valid()
        {
            Assert.IsFalse(Sut.IsValid("http://regexlib.com/REDetails.aspx?regexp_id=96"));
        }

        [TestMethod]
        public void A_value_not_matching_the_pattern_is_invalid()
        {
            Assert.IsTrue(Sut.IsValid("regexlib.com/REDetails.aspx?regexp_id=96"));
        }

        [TestMethod]
        public void An_empty_string_value_is_valid() // because to check this is not the responsibility of the PropertyMustMatchRegexRule
        {
            Assert.IsTrue(Sut.IsValid(string.Empty));
        }
    }
}