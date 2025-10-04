using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    class GenericRule<TProperty> : Rule
    {
        readonly Func<TProperty, bool> assertFunc;
        readonly Func<string> errorMessageFormatFunc;

        public override Type PropertyType => typeof(TProperty);

        public GenericRule(Func<TProperty, bool> assertFunc, Func<string> errorMessageFormatFunc)
        {
            this.assertFunc = assertFunc;
            this.errorMessageFormatFunc = errorMessageFormatFunc;
        }

        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, errorMessageFormatFunc(), name);
        public override bool IsValid(object value) => assertFunc((TProperty)value);

        protected override bool EqualsRule(Rule rule)
        {
            var genericRule = rule as GenericRule<TProperty>;
            return genericRule == null ? false : genericRule.assertFunc.Equals(assertFunc);
        }
    }
}