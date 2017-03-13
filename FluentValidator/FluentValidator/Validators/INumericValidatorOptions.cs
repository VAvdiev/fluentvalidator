using System;

namespace FluentValidator.Validators
{
    public interface INumericValidatorOptions<in T>  where T : IComparable 
    {
        INumericValidatorOptions<T> GreaterThan(T value);
        INumericValidatorOptions<T> LessThan(T value);

        INumericValidatorOptions<T> WithMessage(string message);
        INumericValidatorOptions<T> StopOnFirstFailure();
    }
}