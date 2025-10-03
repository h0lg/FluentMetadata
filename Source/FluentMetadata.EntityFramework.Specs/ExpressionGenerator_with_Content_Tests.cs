using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public class ExpressionGenerator_with_Content_Tests
    {
        [TestMethod]
        public void Generate_for_Id()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Id"));
        }

        [TestMethod]
        public void Generate_for_Created()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Created"));
        }

        [TestMethod]
        public void Generate_for_Title()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Title"));
        }

        [TestMethod]
        public void Generate_for_Author()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Author"));
        }

        [TestMethod]
        public void Generate_for_Comments()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Comments"));
        }

        [TestMethod]
        public void Generate_for_WebSite()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "WebSite"));
        }
    }
}