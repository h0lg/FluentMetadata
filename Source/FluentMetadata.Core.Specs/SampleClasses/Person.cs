
namespace FluentMetadata.Specs.SampleClasses
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}