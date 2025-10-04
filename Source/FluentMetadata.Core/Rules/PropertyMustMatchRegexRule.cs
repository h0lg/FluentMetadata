using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRegexRule : Rule
    {
        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholder {0} for the display name of the property.</summary>
        public static Func<string> GetErrorMessageFormat = () => "the value for {0} is not in the correct format";

        internal readonly string Pattern;

        public override Type PropertyType => typeof(string);

        public PropertyMustMatchRegexRule(string pattern) => Pattern = pattern;

        public override bool IsValid(object value)
        {
            var valueAsString = Convert.ToString(value, CultureInfo.CurrentCulture);

            // because validating this is not the responsibility of the PropertyMustMatchRegexRule
            if (string.IsNullOrEmpty(valueAsString)) return true;

            return Matches(valueAsString);
        }

        protected bool Matches(string value) => new Regex(Pattern).Match(value).Success;

        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, GetErrorMessageFormat(), name);

        protected override bool EqualsRule(Rule rule)
        {
            var regexRule = rule as PropertyMustMatchRegexRule;
            return regexRule == null ? false : regexRule.Pattern.Equals(Pattern);
        }
    }

    public class PropertyMustNotMatchRegexRule : PropertyMustMatchRegexRule
    {
        public PropertyMustNotMatchRegexRule(string pattern) : base(pattern) { }

        public override bool IsValid(object value)
        {
            var valueAsString = Convert.ToString(value, CultureInfo.CurrentCulture);

            // because validating this is not the responsibility of the PropertyMustNotMatchRegexRule
            if (string.IsNullOrEmpty(valueAsString)) return true;

            return !Matches(valueAsString);
        }
    }
}