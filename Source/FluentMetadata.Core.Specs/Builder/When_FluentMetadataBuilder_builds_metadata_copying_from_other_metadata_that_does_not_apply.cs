using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply
    {
        private readonly List<Type> builtMetadata = FluentMetadataBuilder.BuiltMetadataDefininitions;
        private readonly IEnumerable<IRule> someViewModelRules, someViewModelMyPropertyRules;
        private readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply()
        {
            FluentMetadataBuilder.Reset();

            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(GetUnbuildableMetadataDefinitions());
                someViewModelRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel)).Rules;
                someViewModelMyPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyProperty").Rules;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [TestMethod]
        public void It_does_not_throw_an_exception()
        {
            Assert.IsNull(exception);
        }

        [TestMethod]
        public void It_does_not_copy_generic_class_rules()
        {
            Assert.AreEqual(0, someViewModelRules.OfType<GenericClassRule<SomeDomainModel>>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_PropertyMustBeLessThanOtherRules()
        {
            Assert.AreEqual(0, someViewModelRules.OfType<PropertyMustBeLessThanOtherRule<SomeDomainModel>>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_PropertyMustMatchRules()
        {
            Assert.AreEqual(0, someViewModelRules.OfType<PropertyMustMatchRule<SomeDomainModel>>().Count());
        }

        [TestMethod]
        public void It_does_copy_RequiredRules()
        {
            Assert.AreEqual(1, someViewModelMyPropertyRules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_PropertyMustMatchRegexRules()
        {
            Assert.AreEqual(0, someViewModelMyPropertyRules.OfType<PropertyMustMatchRegexRule>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_RangeRules()
        {
            Assert.AreEqual(0, someViewModelMyPropertyRules.OfType<RangeRule>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_StringLengthRules()
        {
            Assert.AreEqual(0, someViewModelMyPropertyRules.OfType<StringLengthRule>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_generic_property_rules()
        {
            Assert.AreEqual(0, someViewModelMyPropertyRules.OfType<GenericRule<int>>().Count());
        }

        [TestMethod]
        public void It_does_not_copy_ClassRuleValidatingAPropertyWrapper()
        {
            Assert.AreEqual(0, someViewModelMyPropertyRules.OfType<ClassRuleValidatingAPropertyWrapper>().Count());
        }

        #region System under test

        private class SomeDomainModel
        {
            public int MyProperty { get; set; }
            public int MyProperty2 { get; set; }
        }
        private class SomeViewModel
        {
            public string MyProperty { get; set; }
        }
        private class SomeDomainModelMetadata : ClassMetadata<SomeDomainModel>
        {
            public SomeDomainModelMetadata()
            {
                Class
                    .AssertThat(
                        svm => false,
                        string.Empty)
                    .ComparableProperty(svm => svm.MyProperty)
                        .ShouldBeLessThan(svm => svm.MyProperty2)
                    .Property(svm => svm.MyProperty)
                        .ShouldEqual(svm => svm.MyProperty2);
                Property(svm => svm.MyProperty)
                    .Is.Required()
                    .Should.MatchRegex("here be some regex")
                    .Range(0, 1)
                    .Length(1, 1)
                    .AssertThat(
                        v => v > 100,
                        string.Empty);
            }
        }
        private class SomeViewModelMetadata : ClassMetadata<SomeViewModel>
        {
            public SomeViewModelMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
            }
        }

        #endregion
    }
}