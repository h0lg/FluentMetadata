using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    class GenericModelValidator : ModelValidator
    {
        readonly Func<object, bool> isValid;
        readonly Func<string> getErrorMessageFormatFromResource;

        internal GenericModelValidator(
            ModelMetadata metadata,
            ControllerContext context,
            Func<string> getErrorMessageFormatFromResource,
            Func<object, bool> isValid)
            : base(metadata, context)
        {
            this.isValid = isValid;
            this.getErrorMessageFormatFromResource = getErrorMessageFormatFromResource;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (isValid(container))
            {
                yield return new ModelValidationResult
                {
                    Message = FormatErrorMessage()
                };
            }
        }

        string FormatErrorMessage()
        {
            return string.Format(getErrorMessageFormatFromResource(), Metadata.DisplayName);
        }
    }
}