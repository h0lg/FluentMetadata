using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Builder
{
    [TestClass]
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata
    {
        private readonly List<Type> builtMetadata = FluentMetadataBuilder.BuiltMetadataDefininitions;
        private readonly IEnumerable<IRule> someViewModelRules, someViewModelMyPropertyRules, someViewModelMyStringPropertyRules;
        private readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata()
        {
            FluentMetadataBuilder.Reset();
            var typesToBuildInLaterBatch = new[] { typeof(SomeTypeInAnotherAssemblyMetadata) };
            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(
                    GetUnbuildableMetadataDefinitions()
                        .Except(typesToBuildInLaterBatch));
                FluentMetadataBuilder.BuildMetadataDefinitions(typesToBuildInLaterBatch);
                someViewModelRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel)).Rules;
                someViewModelMyPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyProperty").Rules;
                someViewModelMyStringPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyStringProperty").Rules;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata);
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
        public void Dependent_metadata_may_be_built_before_its_dependency()
        {
            Assert.IsTrue(
                builtMetadata.IndexOf(typeof(SomeViewModelMetadata)) <
                builtMetadata.IndexOf(typeof(SomeDomainModelMetadata)));
        }

        [TestMethod]
        public void Dependent_metadata_is_built_again_after_its_dependencies_because_it_copies_metadata_from_them()
        {
            Assert.AreEqual(2, builtMetadata.Count(t => t == typeof(SomeViewModelMetadata)));
            Assert.IsTrue(
                builtMetadata.LastIndexOf(typeof(SomeDomainModelMetadata)) <
                builtMetadata.LastIndexOf(typeof(SomeViewModelMetadata)));
        }

        [TestMethod]
        public void Open_generic_metadata_is_built_before_non_generic_metadata()
        {
            Assert.IsTrue(
                builtMetadata.IndexOf(typeof(SomeDomainBaseTypeMetadata<>)) <
                builtMetadata.IndexOf(typeof(SomeDomainModelMetadata)));
        }

        [TestMethod]
        public void Dependent_metadata_is_built_again_if_dependency_is_built_again()
        {
            Assert.AreEqual(2, builtMetadata.Count(t => t == typeof(SomeOtherViewModelMetadata)));
            Assert.IsTrue(
                builtMetadata.LastIndexOf(typeof(SomeViewModelMetadata)) <
                builtMetadata.LastIndexOf(typeof(SomeOtherViewModelMetadata)));
        }

        [TestMethod]
        public void Dependent_metadata_built_in_a_later_batch_is_built_correctly()
        {
            Assert.AreEqual(1, builtMetadata.Count(t => t == typeof(SomeTypeInAnotherAssemblyMetadata)));
        }

        [TestMethod]
        public void It_does_not_duplicate_generic_class_rules()
        {
            Assert.AreEqual(1, someViewModelRules.OfType<GenericClassRule<SomeViewModel>>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_PropertyMustBeLessThanOtherRules()
        {
            Assert.AreEqual(1, someViewModelRules.OfType<PropertyMustBeLessThanOtherRule<SomeViewModel>>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_PropertyMustMatchRules()
        {
            Assert.AreEqual(1, someViewModelRules.OfType<PropertyMustMatchRule<SomeViewModel>>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_RequiredRules()
        {
            Assert.AreEqual(1, someViewModelMyPropertyRules.OfType<RequiredRule>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_PropertyMustMatchRegexRules()
        {
            Assert.AreEqual(1, someViewModelMyStringPropertyRules.OfType<PropertyMustMatchRegexRule>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_RangeRules()
        {
            Assert.AreEqual(1, someViewModelMyPropertyRules.OfType<RangeRule>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_StringLengthRules()
        {
            Assert.AreEqual(1, someViewModelMyStringPropertyRules.OfType<StringLengthRule>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_generic_property_rules()
        {
            Assert.AreEqual(1, someViewModelMyPropertyRules.OfType<GenericRule<int>>().Count());
        }

        [TestMethod]
        public void It_does_not_duplicate_ClassRuleValidatingAPropertyWrapper()
        {
            Assert.AreEqual(1, someViewModelMyPropertyRules.OfType<ClassRuleValidatingAPropertyWrapper>().Count());
        }

        #region System under test

        #region dependent metadata is defined before its dependency

        private class SomeDomainModel : SomeDomainBaseType
        { }
        private class SomeViewModel
        {
            public int MyProperty { get; set; }
            public int MyProperty2 { get; set; }
            public string MyStringProperty { get; set; }
        }
        private class SomeViewModelMetadata : ClassMetadata<SomeViewModel>
        {
            public SomeViewModelMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
                Class
                    .AssertThat(
                        svm => false,
                        string.Empty)
                    .ComparableProperty(svm => svm.MyProperty)
                        .ShouldBeLessThan(svm => svm.MyProperty2)
                    .Property(svm => svm.MyProperty)
                       .ShouldEqual(svm => svm.MyStringProperty);
                Property(svm => svm.MyProperty)
                    .Is.Required()
                    .Range(0, 1)
                    .AssertThat(
                        v => v > 100,
                        string.Empty);
                Property(svm => svm.MyStringProperty)
                    .Should.MatchRegex("here be some regex")
                    .Length(1, 1);
            }
        }
        private class SomeDomainModelMetadata : SomeDomainBaseTypeMetadata<SomeDomainModel>
        { }

        #endregion

        #region open generic metadata is defined after non generic metadata

        private class SomeDomainBaseType
        { }
        private class SomeDomainBaseTypeMetadata<T> : ClassMetadata<T> where T : SomeDomainBaseType
        { }

        #endregion

        #region metadata depentent on incorrectly build metadata

        private class SomeOtherViewModel
        { }
        private class SomeOtherViewModelMetadata : ClassMetadata<SomeOtherViewModel>
        {
            public SomeOtherViewModelMetadata()
            {
                CopyMetadataFrom<SomeViewModel>();
            }
        }

        #endregion

        #region metadata built in a later batch

        private class SomeTypeInAnotherAssembly
        { }
        private class SomeTypeInAnotherAssemblyMetadata : ClassMetadata<SomeTypeInAnotherAssembly>
        {
            public SomeTypeInAnotherAssemblyMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
            }
        }

        #endregion

        #endregion
    }
}