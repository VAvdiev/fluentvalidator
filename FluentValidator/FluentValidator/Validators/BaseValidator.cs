using System;
using System.Collections.Generic;

namespace FluentValidator.Validators
{
    public abstract class BaseValidator : IValidator
    {
        protected List<ValidationRule> ValidationRules = new List<ValidationRule>();
        private readonly List<string> _validationFailures;
        protected IValidationRule CurrentValidationRule { get; set; }


        protected BaseValidator(string fieldName)
        {
            _validationFailures = new List<string>();
            FieldName = fieldName;
            IsValid = true;
        }

        protected BaseValidator(Func<object, object> getter, string fieldName):this(fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
            Getter = getter;
        }

        protected void SetFailure(string message)
        {
            ValidationMessage = message;
            _validationFailures.Add(message);
            IsValid = false;
        }
        public bool IsValid { get; protected set; }
        public string ValidationMessage { get; protected set; }
        public IEnumerable<string> ValidationFailures { get { return _validationFailures; } }
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

        protected TValidator  WithMessageInt<TValidator>(string message) where TValidator: BaseValidator
        {
            CurrentValidationRule.WithMessage(message);
            return (TValidator)this;
        }
        protected IValidationRule AddRule<T>(Func<T, bool> pred)
        {
            var validationRule = new ValidationRule(o => pred((T)o));
            ValidationRules.Add(validationRule);
            CurrentValidationRule = validationRule;
            return validationRule;
        }

        public void Reset()
        {
            ValidationMessage = string.Empty;
            IsValid = true;
        }
    }
}