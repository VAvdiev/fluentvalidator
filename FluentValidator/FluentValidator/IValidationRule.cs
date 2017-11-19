using System;

namespace FluentValidator
{
    public interface IValidationRule
    {
        string Message { get; }
        Func<object, bool> RulePredicate { get; set; }
        Func<object, bool> Predicate { get; }
        void WithMessage(string message);
        void WithMessage(string format, params object[] args);
        void WhenPredicate(Func<object, bool> predicate);
    }
}