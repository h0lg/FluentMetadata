using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    [TestClass]
    public class When_two_properties_should_be_equal
    {
        private readonly PropertyMustMatchRule<ChangePasswordModel> Sut;

        public When_two_properties_should_be_equal()
        {
            Sut = new PropertyMustMatchRule<ChangePasswordModel>(x => x.NewPassword, x => x.ConfirmPassword);
        }

        [TestMethod]
        public void Should_be_valid_if_properties_match()
        {
            var model = new ChangePasswordModel { NewPassword = "asdf", ConfirmPassword = "asdf" };
            Assert.IsTrue(Sut.IsValid(model));
        }

        [TestMethod]
        public void Should_be_invalid_if_properties_do_not_match()
        {
            var model = new ChangePasswordModel { NewPassword = "qwer", ConfirmPassword = "asdf" };
            Assert.IsFalse(Sut.IsValid(model));
        }
    }
}