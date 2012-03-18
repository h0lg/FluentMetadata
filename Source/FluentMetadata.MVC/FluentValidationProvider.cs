using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    public class FluentValidationProvider : ModelValidatorProvider
    {
        public static bool AddImplicitRequiredValidatorsForValueTypes = true;

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var validators = new List<ModelValidator>();

            if (metadata is FluentModelMetadata)
            {
                var isPropertyMetadata = !string.IsNullOrEmpty(metadata.PropertyName);
                var rules = (metadata as FluentModelMetadata).Metadata.Rules;

                if (isPropertyMetadata)
                {
                    if (AddImplicitRequiredValidatorsForValueTypes &&
                        metadata.IsRequired &&
                        !rules.OfType<RequiredRule>().Any())
                    {
                        rulesList.Add(new RequiredRule());
                    }

                    validators.AddRange(rules.Select(rule => new RuleModelValidator(rule, metadata, context)));

                    if (metadata.DataTypeName != null)
                    {
                        validators.Add(
                            new GenericModelValidator(
                                metadata,
                                context,
                                () => "The DataType of {0} is invalid.",
                                (object value) => !string.IsNullOrEmpty(metadata.DataTypeName)));
                    }
                }
                else
                {
                    validators.AddRange(rules.Select(rule => new ClassRuleModelValidator(rule as IClassRule, metadata, context)));
                }
            }

            return validators;
        }
    }
}