using System;

namespace FluentValidator
{
    public interface IValidationRule
    {
        string Message { get; }
        Func<object, bool> RulePredicate { get; set; }
        void WithMessage(string message);
        void WithMessage(string format, params object[] args);
    }
}