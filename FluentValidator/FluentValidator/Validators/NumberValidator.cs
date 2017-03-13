using System;

namespace FluentValidator.Validators
{
    public class NumberValidator : BaseValidator
    {
        public NumberValidator(Func<object, int> getter, string fieldName) : base(o => getter(o),fieldName)
        {
        }

        public NumberValidator GreaterThan(int val)
        {
            AddRule<int>(x => x < val)
                .WithMessage("The value of {0} must be greater than " + val, FieldName);
            return this;
        }

        public NumberValidator LessThan(int val)
        {
            AddRule<int>(x => x > val)
                .WithMessage("The value of {0} must be less than " + val, FieldName);
            return this;
        }

        public NumberValidator WithMessage(string message)
        {
            return WithMessageInt<NumberValidator>(message);
        }

        public NumberValidator StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<NumberValidator>();
        }
    }
}