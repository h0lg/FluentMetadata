using FluentMetadata.Rules;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FluentMetadata.MVC
{
    public class FluentValidationProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            // In Core, ModelMetadata is accessible from context
            var metadata = context.ModelMetadata;

            if (metadata is FluentModelMetadata fluentMetadata)
            {
                var isPropertyMetadata = metadata.ContainerType != null && metadata.PropertyName != null;
                var rules = fluentMetadata.Metadata.Rules;

                if (isPropertyMetadata)
                {
                    foreach (var rule in rules)
                    {
                        context.Results.Add(new ValidatorItem
                        {
                            Validator = new RuleModelValidator(rule),
                            IsReusable = true
                        });
                    }
                }
                else
                {
                    foreach (var rule in rules.OfType<IClassRule>())
                    {
                        context.Results.Add(new ValidatorItem
                        {
                            Validator = new ClassRuleModelValidator(rule),
                            IsReusable = true
                        });
                    }
                }
            }
        }
    }
}