using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class BaseClass_Tests : MetadataTestBase
    {
        private readonly Metadata id, active;

        public BaseClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(BaseClass), nameof(BaseClass.Id));
            active = QueryFluentMetadata.GetMetadataFor(typeof(BaseClass), nameof(BaseClass.Active));
        }

        [TestMethod]
        public void Id_Required_is_True()
        {
            Assert.IsTrue(id.Required!.Value);
        }

        [TestMethod]
        public void Active_Required_is_Null()
        {
            Assert.IsNull(active.Required);
        }
    }

    public class DerivedClass_Tests : MetadataTestBase
    {
        private readonly Metadata id, title;

        public DerivedClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(DerivedClass), nameof(DerivedClass.Id));
            title = QueryFluentMetadata.GetMetadataFor(typeof(DerivedClass), nameof(DerivedClass.Title));
        }

        [TestMethod]
        public void Title_Required_is_true()
        {
            Assert.IsTrue(title.Required!.Value);
        }

        [TestMethod]
        public void Id_Required_is_True()
        {
            Assert.IsTrue(id.Required!.Value);
        }
    }

    public class DerivedDerivedClass_Tests : MetadataTestBase
    {
        private readonly Metadata id, title;

        public DerivedDerivedClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(DerivedDerivedClass), nameof(DerivedDerivedClass.Id));
            title = QueryFluentMetadata.GetMetadataFor(typeof(DerivedDerivedClass), nameof(DerivedDerivedClass.Title));
        }

        [TestMethod]
        public void Title_Required_is_true()
        {
            Assert.IsTrue(title.Required!.Value);
        }

        [TestMethod]
        public void Id_Required_is_True()
        {
            Assert.IsTrue(id.Required!.Value);
        }
    }
}