using System;
using Xunit;

namespace FluentMetadata.Specs
{
    public abstract class MetadataTestBase : IClassFixture<MetadataSetup>
    {
        Exception exception;

        public MetadataTestBase(MetadataSetup data)
        {
            exception = data.Exception;
        }

        [Fact]
        public void MetadataSetupDoesNotThrowAnException()
        {
            Assert.Null(exception);
        }
    }
}