using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComparingMetadata
    {
        protected readonly ModelMetadataProvider OriginalProvider = new DataAnnotationsModelMetadataProvider();
        protected readonly FluentMetadataProvider Sut = new();

        protected ModelMetadata Fluent;
        protected ModelMetadata Expected;

        [TestMethod]
        public void MetadataSetupDoesNotThrowAnException()
        {
            Assert.IsNull(GlobalTestSetup.MetadataSetupException);
        }

        [TestMethod]
        public void Equals_ModelMetadata_Properties_Count()
        {
            Console.WriteLine(Expected.Properties.Count());
            Assert.AreEqual(Expected.Properties.Count(), Fluent.Properties.Count());
        }

        [TestMethod]
        public void Equals_PropertyName()
        {
            Console.WriteLine(Expected.PropertyName);
            Assert.AreEqual(Expected.PropertyName, Fluent.PropertyName);
        }

        [TestMethod]
        public void Equals_DisplayName()
        {
            Console.WriteLine(Expected.DisplayName);
            Assert.AreEqual(Expected.DisplayName, Fluent.DisplayName);
        }

        [TestMethod]
        public void Equals_Model()
        {
            Console.WriteLine(Expected.Model);
            Assert.AreEqual(Expected.Model, Fluent.Model);
        }

        [TestMethod]
        public void Equals_ContainerType()
        {
            Console.WriteLine(Expected.ContainerType);
            Assert.AreEqual(Expected.ContainerType, Fluent.ContainerType);
        }

        [TestMethod]
        public void Equals_ConvertEmptyStringToNull()
        {
            Console.WriteLine(Expected.ConvertEmptyStringToNull);
            Assert.AreEqual(Expected.ConvertEmptyStringToNull, Fluent.ConvertEmptyStringToNull);
        }

        [TestMethod]
        public void Equals_DataTypeName()
        {
            Console.WriteLine(Expected.DataTypeName);
            Assert.AreEqual(Expected.DataTypeName, Fluent.DataTypeName);
        }

        [TestMethod]
        public void Equals_DispalyFormatString()
        {
            Console.WriteLine(Expected.DisplayFormatString);
            Assert.AreEqual(Expected.DisplayFormatString, Fluent.DisplayFormatString);
        }

        [TestMethod]
        public void Equals_Description()
        {
            Console.WriteLine(Expected.Description);
            Assert.AreEqual(Expected.Description, Fluent.Description);
        }

        [TestMethod]
        public void Equals_EditFormatString()
        {
            Console.WriteLine(Expected.EditFormatString);
            Assert.AreEqual(Expected.EditFormatString, Fluent.EditFormatString);
        }

        [TestMethod]
        public void Equals_HideSurroundingHtml()
        {
            Console.WriteLine(Expected.HideSurroundingHtml);
            Assert.AreEqual(Expected.HideSurroundingHtml, Fluent.HideSurroundingHtml);
        }

        [TestMethod]
        public void Equals_IsReadOnly()
        {
            Console.WriteLine(Expected.IsReadOnly);
            Assert.AreEqual(Expected.IsReadOnly, Fluent.IsReadOnly);
        }

        [TestMethod]
        public void Equals_IsRequired()
        {
            Console.WriteLine(Expected.IsRequired);
            Assert.AreEqual(Expected.IsRequired, Fluent.IsRequired);
        }

        [TestMethod]
        public void Equals_ModelType()
        {
            Console.WriteLine(Expected.ModelType);
            Assert.AreEqual(Expected.ModelType, Fluent.ModelType);
        }

        [TestMethod]
        public void Equals_NullDisplayText()
        {
            Console.WriteLine(Expected.NullDisplayText);
            Assert.AreEqual(Expected.NullDisplayText, Fluent.NullDisplayText);
        }

        [TestMethod]
        public void Equals_RequestValidationEnabled()
        {
            Console.WriteLine(Expected.RequestValidationEnabled);
            Assert.AreEqual(Expected.RequestValidationEnabled, Fluent.RequestValidationEnabled);
        }

        [TestMethod]
        public void Equals_ShowForDisplay()
        {
            Console.WriteLine(Expected.ShowForDisplay);
            Assert.AreEqual(Expected.ShowForDisplay, Fluent.ShowForDisplay);
        }

        [TestMethod]
        public void Equals_ShowForEdit()
        {
            Console.WriteLine(Expected.ShowForEdit);
            Assert.AreEqual(Expected.ShowForEdit, Fluent.ShowForEdit);
        }

        [TestMethod]
        public void Equals_TemplateHint()
        {
            Console.WriteLine(Expected.TemplateHint);
            Assert.AreEqual(Expected.TemplateHint, Fluent.TemplateHint);
        }

        [TestMethod]
        public void Equals_Watermark()
        {
            Console.WriteLine(Expected.Watermark);
            Assert.AreEqual(Expected.Watermark, Fluent.Watermark);
        }

        [TestMethod]
        public void DataAnnotationsModelValidatorProviderAppliesRequiredValidators()
        {
            var controllerContext = new ControllerContext();
            var dataAnnotationsModelValidatorProvider = new DataAnnotationsModelValidatorProvider();

            var expectedValidatorCount = dataAnnotationsModelValidatorProvider
                .GetValidators(Expected, controllerContext)
                .Count(v => v.IsRequired);

            if (Expected.IsRequired)
            {
                Assert.AreEqual(1, expectedValidatorCount);
            }
            else
            {
                RangeAssert.InRange(expectedValidatorCount, 0, 1);
            }

            Assert.AreEqual(expectedValidatorCount,
                dataAnnotationsModelValidatorProvider
                    .GetValidators(Fluent, controllerContext)
                    .Count(v => v.IsRequired));
        }

        [TestMethod]
        public void StringLengthValidatorsMatch()
        {
            var controllerContext = new ControllerContext();

            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<StringLengthAttributeAdapter>()
                .Count();

            RangeAssert.InRange(expectedValidatorCount, 0, 1);

            Assert.AreEqual(expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationStringLengthRule>()
                    .Count());
        }

        [TestMethod]
        public void RangeValidatorsMatch()
        {
            var controllerContext = new ControllerContext();

            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<RangeAttributeAdapter>()
                .Count();

            RangeAssert.InRange(expectedValidatorCount, 0, 1);

            Assert.AreEqual(expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationRangeRule>()
                    .Count());
        }

        [TestMethod]
        public void RegularExpressionValidatorsMatch()
        {
            var controllerContext = new ControllerContext();

            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<RegularExpressionAttributeAdapter>()
                .Count();

            RangeAssert.InRange(expectedValidatorCount, 0, 1);

            Assert.AreEqual(expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationRegexRule>()
                    .Count());
        }

        [TestMethod]
        public void EqualToClientValidationRulesMatch()
        {
            var controllerContext = new ControllerContext();

            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .SelectMany(rmv => rmv.GetClientValidationRules())
                .OfType<ModelClientValidationEqualToRule>()
                .Count();

            RangeAssert.InRange(expectedValidatorCount, 0, 1);

            Assert.AreEqual(expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationEqualToRule>()
                    .Count());
        }
    }

    public static class RangeAssert
    {
        public static void InRange<T>(T actual, T low, T high) where T : IComparable<T>
        {
            if (actual.CompareTo(low) < 0 || actual.CompareTo(high) > 0)
            {
                Assert.Fail($"Value {actual} was not in range {low} to {high}.");
            }
        }
    }
}