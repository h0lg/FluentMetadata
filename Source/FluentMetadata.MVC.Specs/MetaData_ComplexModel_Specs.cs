using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComplexModel : ConcernOfComparingMetadata
    {
        protected ComplexModel model;

        protected ConcernOfComplexModel()
        {
            model = new ComplexModel()
            {
                FirstName = "Albert",
                LastName = "Weinert",
                Age = 39,
                Sex = 'm',
                Amount = 815.4711m
            };
        }

        protected void CreatePropertyMetadata(string propertyName)
        {
            Fluent = Sut.GetMetadataForProperty(() => model, model.GetType(), propertyName);
            Expected = OriginalProvider.GetMetadataForProperty(() => model, model.GetType(), propertyName);
        }
    }

    [TestClass]
    public class When_getting_the_Metadata_of_the_Type_ComplexModel : ConcernOfComplexModel
    {
        private readonly ModelValidator[] validators;

        public When_getting_the_Metadata_of_the_Type_ComplexModel()
        {
            model.FirstName = "Robert'); DROP ";
            model.LastName = "TABLE Students; --";
            Fluent = Sut.GetMetadataForType(() => model, model.GetType());
            Expected = OriginalProvider.GetMetadataForType(() => model, model.GetType());

            validators = new FluentValidationProvider()
                .GetValidators(Fluent, new ControllerContext())
                .ToArray();
        }

        [TestMethod]
        public void A_validator_is_returned_for_the_generic_rule() => Assert.AreEqual(1, validators.Length);

        [TestMethod]
        public void The_validator_is_of_type_ClassRuleModelValidator() => Assert.IsInstanceOfType<ClassRuleModelValidator>(validators[0]);

        [TestMethod]
        public void The_validator_returns_1_ModelValidationResult() => Assert.AreEqual(1, validators[0].Validate(model).Count());

        [TestMethod]
        public void The_error_message_of_the_ModelValidationResult_equals_the_message_specified_in_the_rule()
            => Assert.AreEqual("'LastName' and 'Vorname' do not match.", validators[0].Validate(model).ToArray()[0].Message);

        [TestMethod]
        public void Getting_metadata_for_all_properties_does_not_throw_an_exception()
        {
            Exception error = null;
            IEnumerable<ModelMetadata> properties = null;

            try { properties = Sut.GetMetadataForProperties(model, model.GetType()); }
            catch (Exception ex) { error = ex; }

            Assert.IsNull(error);
            Assert.AreEqual(OriginalProvider.GetMetadataForProperties(model, model.GetType()).Count(), properties.Count());
        }
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Id : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_Id() => CreatePropertyMetadata("Id");
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_FirstName : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_FirstName() => CreatePropertyMetadata("FirstName");
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_LastName : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_LastName() => CreatePropertyMetadata("LastName");
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Sex : ConcernOfComplexModel
    {
        private readonly ModelValidator[] validators;

        public When_getting_the_Metadata_of_ComplexModel_Property_Sex()
        {
            CreatePropertyMetadata("Sex");

            validators = new FluentValidationProvider()
                .GetValidators(Fluent, new ControllerContext())
                .ToArray();
        }

        [TestMethod]
        public void A_validator_is_returned_for_the_generic_rule() => Assert.AreEqual(1, validators.Length);

        [TestMethod]
        public void The_validator_is_of_type_RuleModelValidator() => Assert.IsInstanceOfType<RuleModelValidator>(validators[0]);

        [TestMethod]
        public void The_validator_returns_1_ModelValidationResult() => Assert.AreEqual(1, validators[0].Validate(model).Count());

        [TestMethod]
        public void The_error_message_of_the_ModelValidationResult_says_value_cannot_be_male()
            => Assert.AreEqual("'Sex' cannot be male since this is a ComplexModel.",
                validators[0].Validate(model).ToArray()[0].Message);
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Amount : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_Amount() => CreatePropertyMetadata("Amount");
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Age : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_Age() => CreatePropertyMetadata("Age");
    }

    [TestClass]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Active : ConcernOfComplexModel
    {
        public When_getting_the_Metadata_of_ComplexModel_Property_Active() => CreatePropertyMetadata("Active");
    }
}