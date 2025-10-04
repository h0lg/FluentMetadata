namespace FluentMetadata.MVC
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

    public class FluentMetadataProvider : IDisplayMetadataProvider, IValidationMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyName = context.Key.Name;
            var containerType = context.Key.ContainerType;
            var modelType = context.Key.ModelType;

            // Pull Fluent metadata
            var fluentMetadata = propertyName == null
                ? QueryFluentMetadata.GetMetadataFor(modelType)
                : QueryFluentMetadata.GetMetadataFor(containerType, propertyName);

            if (fluentMetadata != null)
            {
                // Example: set display name & description
                context.DisplayMetadata.DisplayName = () => fluentMetadata.DisplayName;
                context.DisplayMetadata.Description = () => fluentMetadata.Description;
            }
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            var propertyName = context.Key.Name;
            var containerType = context.Key.ContainerType;
            var modelType = context.Key.ModelType;

            var fluentMetadata = propertyName == null
                ? QueryFluentMetadata.GetMetadataFor(modelType)
                : QueryFluentMetadata.GetMetadataFor(containerType, propertyName);

            if (fluentMetadata != null)
            {
                // Example: attach Fluent validation attributes
                foreach (var rule in fluentMetadata.Rules)
                {
                    context.ValidationMetadata.ValidatorMetadata.Add(rule);
                }
            }
        }
    }
}