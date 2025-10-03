using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    [TestClass]
    public class ExpressionGenerator_with_ContentBase_Tests
    {
        [TestMethod]
        public void Generate_for_Id()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Id"));
        }

        [TestMethod]
        public void Generate_for_Created()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Created"));
        }

        [TestMethod]
        public void Generate_for_Title()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Title"));
        }

        [TestMethod]
        public void Generate_for_Author()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Author"));
        }

        [TestMethod]
        public void Generate_for_Comments()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Comments"));
        }

        [TestMethod]
        public void Generate_for_Layout()
        {
            Assert.IsNotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Layout"));
        }
    }
}