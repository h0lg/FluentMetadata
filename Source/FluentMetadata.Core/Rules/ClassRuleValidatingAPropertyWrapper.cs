using System;

namespace FluentMetadata.Rules
{
    /// <summary>
    /// A wrapper for a class rule validating a property used to relate the class rule to the property it is validating.
    /// </summary>
    class ClassRuleValidatingAPropertyWrapper : Rule
    {
        public override Type PropertyType
        {
            get
            {
                return PropertyValidatingRule.ClassType
                    .GetProperty(PropertyValidatingRule.PropertyName)
                    .PropertyType;
            }
        }

        internal IValidateAProperty PropertyValidatingRule { get; }

        internal ClassRuleValidatingAPropertyWrapper(IValidateAProperty propertyValidatingRule)
            => PropertyValidatingRule = propertyValidatingRule;

        public override bool IsValid(object value)
        {
            //should always validate since it's only a dummy for wrapping an IValidateAProperty
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            throw new NotSupportedException(
@"This rule is only a wrapper for a class rule validating a property.
Please get the exception message from the wrapped class rule.");
        }

        protected override bool EqualsRule(Rule rule)
        {
            var classRule = rule as ClassRuleValidatingAPropertyWrapper;
            return classRule == null ? false : classRule.PropertyValidatingRule.Equals(PropertyValidatingRule);
        }
    }
}