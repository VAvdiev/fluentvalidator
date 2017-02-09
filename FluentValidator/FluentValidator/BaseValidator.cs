using System;
using System.Collections.Generic;

namespace FluentValidator
{
    public abstract class BaseValidator : IValidatorResult
    {
        protected List<ValidationRule> ValidationRules = new List<ValidationRule>();

        protected BaseValidator(string fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
        }

        protected BaseValidator(Func<object, object> getter, string fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
            Getter = getter;
        }

        protected void SetFailure(string message)
        {
            ValidationMessage = message;
            IsValid = false;
        }
        public bool IsValid { get; protected set; }
        public string ValidationMessage { get; protected set; }
        public string FieldName { get; private set; }
        public Func<object, object> Getter { get; set; }

        public void Validate(object entity)
        {
            foreach (var validationRule in ValidationRules)
            {
                if (validationRule.Predicate(Getter(entity)))
                {
                    SetFailure(validationRule.Message);
                }
            }
        }

        protected IValidationRule AddRule<T>(Func<T, bool> pred)
        {
            var validationRule = new ValidationRule(o => pred((T)o));
            ValidationRules.Add(validationRule);

            return validationRule;
        }
    }
}