using System.Linq.Expressions;
using FluentMetadata.FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils.Reflection;

namespace FluentMetadata.FluentNHibernate.Specs
{
    [TestClass]
    public class FluentMetaDataConventionSpecs
    {
        private readonly FluentMetadataConvention sut;

        public FluentMetaDataConventionSpecs()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<TestClassMetadata>();
            sut = new FluentMetadataConvention();
        }

        [TestMethod]
        public void AppliesNotNullToRequiredProperties()
        {
            var idMapping = GetPropertyMapping<TestClass>(t => t.Id);

            sut.Apply(new PropertyInstance(idMapping));

            Assert.IsTrue(idMapping.Columns.Single().NotNull);
        }

        [TestMethod]
        public void AppliesNullToNonRequiredProperties()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.NullableNumber);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.IsFalse(optionalMapping.Columns.Single().NotNull);
        }

        [TestMethod]
        public void AppliesMaximumStringLengthToProperties()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.SomeString);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.AreEqual(42, optionalMapping.Columns.Single().Length);
        }

        [TestMethod]
        public void DoesNotThrowAnExceptionForNotFoundProperties()
        {
            var privateMapping = GetPropertyMapping<TestClass>(TestClass.Expressions.SomePrivateField);
            Exception exception = null;

            try
            {
                sut.Apply(new PropertyInstance(privateMapping));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void AppliesNotNullToRequiredReferences()
        {
            var requiredMapping = GetPropertyMapping<TestClass>(t => t.RequiredReference);

            sut.Apply(new PropertyInstance(requiredMapping));

            Assert.IsTrue(requiredMapping.Columns.Single().NotNull);
        }

        [TestMethod]
        public void AppliesNullToNonRequiredReferences()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.NullableReference);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.IsFalse(optionalMapping.Columns.Single().NotNull);
        }

        private static PropertyMapping GetPropertyMapping<T>(Expression<Func<T, object>> propertyExpression)
        {
            var propertyMapping = new PropertyMapping
            {
                Member = ReflectionHelper.GetMember<T>(propertyExpression),
                ContainingEntityType = typeof(T)
            };
            propertyMapping.AddColumn(0, new ColumnMapping());
            return propertyMapping;
        }
    }

    public class TestClass
    {
        private readonly int somePrivateField = 42;

        public int Id { get; protected set; }
        public int? NullableNumber { get; set; }
        public string SomeString { get; set; }
        public Reference RequiredReference { get; set; }
        public Reference NullableReference { get; set; }

        public class Expressions
        {
            /* this represents a way to map private members using Fluent NHibernate;
             * see http://wiki.fluentnhibernate.org/Fluent_mapping_private_properties */
            public static Expression<Func<TestClass, object>> SomePrivateField = t => t.somePrivateField;
        }
    }

    public class Reference
    {
        public int Id { get; protected set; }
    }

    public class TestClassMetadata : ClassMetadata<TestClass>
    {
        public TestClassMetadata()
        {
            Property(t => t.Id)
                .Is.Required();
            Property(t => t.SomeString)
                .Length(42);
            Property(t => t.RequiredReference)
                .Is.Required();
        }
    }
}