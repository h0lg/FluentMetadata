namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata
    {
        private readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata()
        {
            FluentMetadataBuilder.Reset();

            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(
                    GetUnbuildableMetadataDefinitions());
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [TestMethod]
        public void It_throws_a_NoMetadataDefinedException()
        {
            Assert.IsInstanceOfType<MetadataDefinitionSorter.NoMetadataDefinedException>(exception);
        }

        [TestMethod]
        public void The_NoMetadataDefinedException_contains_the_full_name_of_the_metadata_type_that_is_unbuildable()
        {
            Assert.IsTrue(exception.Message.Contains(typeof(SomeTypeMetadata).FullName));
        }

        [TestMethod]
        public void The_NoMetadataDefinedException_contains_the_full_name_of_the_model_type_whose_metadata_cannot_be_found()
        {
            Assert.IsTrue(exception.Message.Contains(typeof(SomeOtherType).FullName));
        }

        #region metadata copying from non-existing

        private class SomeType
        { }
        private class SomeOtherType
        { }

        private class SomeTypeMetadata : ClassMetadata<SomeType>
        {
            public SomeTypeMetadata()
            {
                CopyMetadataFrom<SomeOtherType>();
            }
        }

        #endregion
    }
}