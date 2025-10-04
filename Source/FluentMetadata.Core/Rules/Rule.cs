using System;

namespace FluentMetadata.Rules
{
    public abstract class Rule : IRule
    {
        public abstract Type PropertyType { get; }

        public abstract bool IsValid(object value);
        public abstract string FormatErrorMessage(string name);
        protected abstract bool EqualsRule(Rule rule);

        public override bool Equals(object obj)
        {
            return EqualsRule(obj as Rule);
        }
    }
}