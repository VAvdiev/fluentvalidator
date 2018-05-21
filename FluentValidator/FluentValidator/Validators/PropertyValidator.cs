using System;
using System.Collections.Generic;

namespace FluentValidator.Validators
{
    public abstract class PropertyValidator : IValidator
    {
        private readonly List<ValidationRule> _validationRules = new List<ValidationRule>();
        private readonly List<ValidationRule> _dependentRules = new List<ValidationRule>();
        private readonly List<string> _validationFailures;
        private bool _stopOnFirstFailure;
        protected IValidationRule CurrentValidationRule { get; set; }


        protected PropertyValidator(string fieldName)
        {
            _validationFailures = new List<string>();
            FieldName = fieldName;
            IsValid = true;
        }

        protected PropertyValidator(Func<object, object> getter, string fieldName)
            : this(fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
            Getter = getter;
        }

        protected void SetFailure(string message)
        {
            _validationFailures.Add(message);
            IsValid = false;
        }
        public bool IsValid { get; protected set; }
        public IEnumerable<string> ValidationFailures { get { return _validationFailures; } }
        public string FieldName { get; private set; }
        public Func<object, object> Getter { get; set; }

        public void Validate(object entity)
        {
            Reset();
            foreach (var validationRule in _validationRules)
            {
                if (!validationRule.Predicate(entity))
                {
                    continue;
                }

                if (validationRule.RulePredicate(Getter(entity)))
                {

                    SetFailure(validationRule.Message);
                    if (_stopOnFirstFailure)
                    {
                        break;
                    }
                }
            }
           
            foreach (var dependentRule in _dependentRules)
            {
                if (!dependentRule.Predicate(entity))
                {
                    continue;
                }
                if (!dependentRule.RulePredicate(entity))
                {
                    SetFailure(dependentRule.Message);
                }
            }
        }

        protected TValidator WithMessageInt<TValidator>(string message) where TValidator : PropertyValidator
        {
            CurrentValidationRule.WithMessage(message);
            return (TValidator)this;
        }
        protected TValidator WithNameInt<TValidator>(string overridenName) where TValidator : PropertyValidator
        {
            FieldName = overridenName;

            return (TValidator)this;
        }

        protected TValidator WhenInt<TValidator>(Func<object, bool> predicate) where TValidator : PropertyValidator
        {
            CurrentValidationRule.WhenPredicate(predicate);
            return (TValidator)this;
        }

        protected TValidator StopOnFirstFailureInt<TValidator>() where TValidator : PropertyValidator
        {
            _stopOnFirstFailure = true;
            return (TValidator)this;
        }
        protected IValidationRule AddRule<T>(Func<T, bool> pred)
        {
            var validationRule = new ValidationRule(o => pred((T)o));
            _validationRules.Add(validationRule);
            CurrentValidationRule = validationRule;
            return validationRule;
        }

        protected IValidationRule AddDendentRule<T>(Func<T, bool> pred)
        {
            var validationRule = new ValidationRule(o => pred((T)o));
            _dependentRules.Add(validationRule);
            CurrentValidationRule = validationRule;
            return validationRule;
        }

        private void Reset()
        {
            _validationFailures.Clear();
            IsValid = true;
        }
    }
}