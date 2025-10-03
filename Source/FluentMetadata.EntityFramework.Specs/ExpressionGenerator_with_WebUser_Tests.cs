using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public class ExpressionGenerator_with_WebUser_Tests
    {
        [TestMethod]
        public void Generate_for_Username()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Username"));
        }

        [TestMethod]
        public void Wont_Generate_for_Dummy()
        {
            Assert.IsNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Dummy"));
        }

        [TestMethod]
        public void Generate_for_BountCount()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Confirmed"));
        }

        [TestMethod]
        public void Generate_for_LastLogin()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "LastLogin"));
        }

        [TestMethod]
        public void Generate_for_ConfirmationKey()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "ConfirmationKey"));
        }
    }
}