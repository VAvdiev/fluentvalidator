using System;

namespace FluentValidator.Validators
{
    public class IntValidator : BaseValidator
    {
        public IntValidator(Func<object, int> getter, string fieldName) : base(o => getter(o),fieldName)
        {
        }

        public IntValidator GreaterThan(int val)
        {
            AddRule<int>(x => x < val)
                .WithMessage("The value of {0} must be greater than " + val, FieldName);
            return this;
        }

        public IntValidator LessThan(int val)
        {
            AddRule<int>(x => x > val)
                .WithMessage("The value of {0} must be less than " + val, FieldName);
            return this;
        }

        public IntValidator WithMessage(string message)
        {
            return WithMessageInt<IntValidator>(message);
        }

    }
}