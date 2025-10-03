using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    [TestClass]
    public class When_the_maximal_StringLength_is_50
    {
        private readonly StringLengthRule Sut;

        public When_the_maximal_StringLength_is_50()
        {
            Sut = new StringLengthRule(50);
        }

        [TestMethod]
        public void Should_Not_Valid_with_a_string_with_length_100()
        {
            var badLength = new string('x', 100);
            Assert.IsFalse(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Valid_with_a_string_with_length_50()
        {
            var badLength = new string('x', 50);
            Assert.IsTrue(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Valid_with_a_string_with_length_25()
        {
            var badLength = new string('x', 25);
            Assert.IsTrue(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Not_Valid_with_a_string_with_length_51()
        {
            var badLength = new string('x', 51);
            Assert.IsFalse(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Valid_with_a_string_with_length_49()
        {
            var badLength = new string('x', 49);
            Assert.IsTrue(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Valid_with_a_string_with_length_0()
        {
            var badLength = string.Empty;
            Assert.IsTrue(Sut.IsValid(badLength));
        }

        [TestMethod]
        public void Should_Valid_with_a_string_is_NULL()
        {
            string badLength = null;
            Assert.IsTrue(Sut.IsValid(badLength));
        }
    }

    [TestClass]
    public class When_the_minimal_StringLength_is_8_and_Maximal_StringLength_is_null
    {
        private readonly StringLengthRule Sut;

        public When_the_minimal_StringLength_is_8_and_Maximal_StringLength_is_null()
        {
            Sut = new StringLengthRule(8, null);
        }

        [TestMethod]
        public void Should_be_invalid_with_a_null_string()
        {
            Assert.IsFalse(Sut.IsValid(null));
        }

        [TestMethod]
        public void Should_be_invalid_with_a_string_with_length_7()
        {
            var value = new string('a', 7);
            Assert.IsFalse(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_valid_with_a_string_with_length_8()
        {
            var value = new string('a', 8);
            Assert.IsTrue(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_valid_with_a_string_with_length_4001()
        {
            var value = new string('a', 4001);
            Assert.IsTrue(Sut.IsValid(value));
        }
    }

    [TestClass]
    public class When_the_minimal_StringLength_is_5_and_Maximal_StringLength_is_250
    {
        private readonly StringLengthRule Sut;

        public When_the_minimal_StringLength_is_5_and_Maximal_StringLength_is_250()
        {
            Sut = new StringLengthRule(5, 250);
        }

        [TestMethod]
        public void Should_be_invalid_with_a_string_with_length_4()
        {
            var value = new string('a', 4);
            Assert.IsFalse(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_valid_with_a_string_with_length_5()
        {
            var value = new string('a', 5);
            Assert.IsTrue(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_valid_with_a_string_with_length_249()
        {
            var value = new string('a', 249);
            Assert.IsTrue(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_valid_with_a_string_with_length_250()
        {
            var value = new string('a', 249);
            Assert.IsTrue(Sut.IsValid(value));
        }

        [TestMethod]
        public void Should_be_invalid_with_a_string_with_length_251()
        {
            var value = new string('a', 251);
            Assert.IsFalse(Sut.IsValid(value));
        }
    }
}