
namespace FluentMetadata.MVC.Specs
{
    [TestClass]
    public class ValidationOfComplexDataTests
    {
        private DummyController _sut;
        private ComplexModel _model;

        [TestInitialize]
        public void Setup()
        {
            _sut = new DummyController();
            _model = new ComplexModel();
        }

        [TestMethod]
        public void When_FirstName_is_required_and_not_set_Should_have_one_error()
        {
            // Act
            _sut.ValidateModel(_model);

            // Assert
            Assert.AreEqual(1, _sut.ModelState["FirstName"].Errors.Count);
        }

        [TestMethod]
        public void When_FirstName_is_required_and_is_set_Should_have_no_error()
        {
            // Arrange
            _model.FirstName = "Albert";

            // Act
            _sut.ValidateModel(_model);

            // Assert
            Assert.IsNull(_sut.ModelState["FirstName"]);
        }
    }
}