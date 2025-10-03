namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references
    {
        private readonly Exception exception;

        public When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references()
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
            var type = typeof(When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [TestMethod]
        public void It_throws_a_CircularRefenceException()
        {
            Assert.IsInstanceOfType<MetadataDefinitionSorter.CircularRefenceException>(exception);
        }

        [TestMethod]
        public void The_CircularRefenceException_contains_the_full_name_of_each_type_building_the_circular_reference()
        {
            GetUnbuildableMetadataDefinitions()
                .ToList()
                .ForEach(t => Assert.IsTrue(exception.Message.Contains(t.FullName)));
        }

        #region metadata with circular references

        private class SomeType
        { }
        private class SomeOtherType
        { }
        private class SomeThirdType
        { }
        private class SomeTypeMetadata : ClassMetadata<SomeType>
        {
            public SomeTypeMetadata()
            {
                CopyMetadataFrom<SomeOtherType>();
            }
        }
        private class SomeOtherTypeMetadata : ClassMetadata<SomeOtherType>
        {
            public SomeOtherTypeMetadata()
            {
                CopyMetadataFrom<SomeThirdType>();
            }
        }
        private class SomeThirdTypeMetadata : ClassMetadata<SomeThirdType>
        {
            public SomeThirdTypeMetadata()
            {
                CopyMetadataFrom<SomeType>();
            }
        }

        #endregion
    }
}