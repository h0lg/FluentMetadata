using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    public abstract class ConcernOfRangeRule(RangeRule sut)
    {
        protected readonly RangeRule Sut = sut;
    }

    [TestClass]
    public class When_the_range_is_between_100_and_200 : ConcernOfRangeRule
    {
        public When_the_range_is_between_100_and_200() : base(new RangeRule(100, 200, typeof(int))) { }

        [TestMethod]
        public void Should_150_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(150));
        }

        [TestMethod]
        public void Should_100_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(100));
        }

        [TestMethod]
        public void Should_200_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(200));
        }

        [TestMethod]
        public void Should_201_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(201));
        }

        [TestMethod]
        public void Should_250_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(250));
        }

        [TestMethod]
        public void Should_50_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(50));
        }

        [TestMethod]
        public void Should_99_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(99));
        }
    }

    [TestClass]
    public class When_the_range_is_between_100_0_and_200_0 : ConcernOfRangeRule
    {
        public When_the_range_is_between_100_0_and_200_0() : base(new RangeRule(100.0, 200.0, typeof(double))) { }

        [TestMethod]
        public void Should_150_0_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(150.0));
        }

        [TestMethod]
        public void Should_100_0_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(100.0));
        }

        [TestMethod]
        public void Should_200_0_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(200.0));
        }

        [TestMethod]
        public void Should_250_0_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(250.0));
        }

        [TestMethod]
        public void Should_50_0_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(50.0));
        }

        [TestMethod]
        public void Should_99_9_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(99.9));
        }

        [TestMethod]
        public void Should_200_1_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(200.1));
        }
    }

    [TestClass]
    public class When_the_range_is_between_1_1_2010_and_5_5_2010 : ConcernOfRangeRule
    {
        public When_the_range_is_between_1_1_2010_and_5_5_2010()
            : base(new RangeRule(new DateTime(2010, 1, 1), new DateTime(2010, 5, 5), typeof(DateTime))) { }

        [TestMethod]
        public void Should_2_2_2010_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(new DateTime(2010, 2, 2)));
        }

        [TestMethod]
        public void Should_1_1_2010_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(new DateTime(2010, 1, 1)));
        }

        [TestMethod]
        public void Should_5_5_2010_is_in_range()
        {
            Assert.IsTrue(Sut.IsValid(new DateTime(2010, 5, 5)));
        }

        [TestMethod]
        public void Should_6_6_2011_0_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(new DateTime(2011, 6, 6)));
        }

        [TestMethod]
        public void Should_1_1_2009_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(new DateTime(2009, 1, 1)));
        }

        [TestMethod]
        public void Should_31_12_2009_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(new DateTime(2009, 12, 31)));
        }

        [TestMethod]
        public void Should_6_5_2010_is_out_of_range()
        {
            Assert.IsFalse(Sut.IsValid(new DateTime(2010, 5, 6)));
        }
    }
}