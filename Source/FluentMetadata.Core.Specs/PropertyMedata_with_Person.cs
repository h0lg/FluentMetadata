using FluentMetadata.Specs.SampleClasses;
using Xunit;
using System;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_Person : MetadataTestBase
    {
        readonly Metadata firstName, lastName;

        public PropertyMedata_with_Person()
        {
            firstName = QueryFluentMetadata.GetMetadataFor(typeof(Person), "FirstName");
            lastName = QueryFluentMetadata.GetMetadataFor(typeof(Person), "LastName");
        }

        [Fact]
        public void FirstName_ModelName_is_FirstName()
        {
            Assert.Equal("FirstName", firstName.ModelName);
        }

        [Fact]
        public void FirstName_ModelType_is_string()
        {
            Assert.Equal(typeof(string), firstName.ModelType);
        }

        [Fact]
        public void FirstName_Required_is_true()
        {
            Assert.True(firstName.Required.Value);
        }

        [Fact]
        public void LastName_ModelName_is_LastName()
        {
            Assert.Equal("LastName", lastName.ModelName);
        }

        [Fact]
        public void LastName_ModelType_is_string()
        {
            Assert.Equal(typeof(string), lastName.ModelType);
        }

        [Fact]
        public void LastName_Required_is_not_set()
        {
            Assert.False(lastName.Required.HasValue);
        }

        [Fact]
        public void Street_with_length_0_is_invalid()
        {
            //TODO implement a way to add specific rules for properties in components
            var person = new Person { Address = new Address { Street = string.Empty } };
            throw new NotImplementedException();
        }
    }
}