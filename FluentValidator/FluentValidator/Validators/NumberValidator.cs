using System;

namespace FluentValidator.Validators
{
    public class NumericValidator<T> : BaseValidator, INumericValidatorOptions<T> where T: IComparable
    {
        public NumericValidator(Func<object, T> getter, string fieldName) : base(o => getter(o),fieldName)
        {
        }

        public INumericValidatorOptions<T> GreaterThan(T value)
        {
            AddRule<T>(x => x.CompareTo(value) < 0 || x.CompareTo(value) == 0)
                .WithMessage("The value of {0} must be greater than " + value, FieldName);
            return this;
        }

        public INumericValidatorOptions<T> LessThan(T value)
        {
            AddRule<T>(x => x.CompareTo(value) > 0 || x.CompareTo(value) == 0)
                .WithMessage("The value of {0} must be less than " + value, FieldName);
            return this;
        }

        public INumericValidatorOptions<T> WithMessage(string message)
        {
            return WithMessageInt<NumericValidator<T>>(message);
        }

        public INumericValidatorOptions<T> StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<NumericValidator<T>>();
        }
    }
}