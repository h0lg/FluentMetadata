using AutoMapper;

namespace FluentMetadata.AutoMapper.Specs
{
    [TestClass]
    public class When_copying_metadata_from_an_AutoMapped_Type
    {
        private readonly Metadata destinationMetadata;

        public When_copying_metadata_from_an_AutoMapped_Type()
        {
            FluentMetadataBuilder.Reset();

            // add all  see https://docs.automapper.org/en/stable/Configuration.html#assembly-scanning-for-auto-configuration
            var config = new MapperConfiguration(cfg =>
            {
                /*  in order to test NonPublic member mapping as well,
                 *  see https://docs.automapper.org/en/stable/Configuration.html#configuring-visibility */
                cfg.ShouldMapProperty = _property => true;

                // add maps, see https://docs.automapper.org/en/stable/Configuration.html#configuration
                cfg.CreateMap<Source, Destination>()
                    .ForMember(d => d.Renamed, o => o.MapFrom(s => s.Named))
                    .ForMember(d => d.IntProperty, o => o.MapFrom<FakeResolver, string>(s => s.StringField));
            });

            MapperConfigurationExtensions.GetMapperConfiguration = () => config;
            config.AssertConfigurationIsValid();
            FluentMetadataBuilder.ForAssemblyOfType<Source>();
            destinationMetadata = QueryFluentMetadata.GetMetadataFor(typeof(Destination));
        }

        [TestMethod]
        public void a_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.AreEqual("pockänsdfsdf", destinationMetadata.Properties[nameof(Destination.MyProperty)].GetDisplayName());
        }

        [TestMethod]
        public void a_non_public_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.AreEqual("non-public", destinationMetadata.Properties[nameof(Destination.NonPublic)].GetDescription());
        }

        [TestMethod]
        public void the_destination_type_should_have_metadata_from_the_source_type_it_is_mapped_to()
        {
            Assert.AreEqual("rzjsfghgafsdfh", destinationMetadata.GetDisplayName());
        }

        [TestMethod]
        public void a_projected_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.AreEqual("adföoiulkanhsda", destinationMetadata.Properties[nameof(Destination.Renamed)].GetDescription());
        }

        [TestMethod]
        public void a_flattened_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.AreEqual(true, destinationMetadata.Properties[nameof(Destination.NestedFurtherNestedId)].Required);
        }

        [TestMethod]
        public void a_destination_property_resolved_from_a_source_property_should_have_metadata_from_the_source_property()
        {
            Assert.AreEqual("üoicvnqwnb", destinationMetadata.Properties[nameof(Destination.IntProperty)].TemplateHint);
        }
    }
}