using System;
using System.Collections.Generic;

namespace FluentValidator
{
    public class BaseValidator : IValidatorResult
    {
        protected List<ValidationRule> ValidationRules = new List<ValidationRule>();

        public BaseValidator(string fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
        }

        protected void SetFailure(string message)
        {
            ValidationMessage = message;
            IsValid = false;
        }
        public bool IsValid { get; protected set; }
        public string ValidationMessage { get; protected set; }
        public string FieldName { get; private set; }
        public void Validate(object entity)
        {
            foreach (var validationRule in ValidationRules)
            {
                if (validationRule.Predicate(entity))
                {
                    SetFailure(validationRule.Message);
                }
            }
        }

        private void AddRule(ValidationRule validationRule)
        {
            ValidationRules.Add(validationRule);
        }

        protected IValidationRule AddRule<T>( Func<T, bool> pred )
        {
            var validationRule = new ValidationRule(o => pred((T)o));
            ValidationRules.Add(validationRule);

            return validationRule;
        }
    }
}