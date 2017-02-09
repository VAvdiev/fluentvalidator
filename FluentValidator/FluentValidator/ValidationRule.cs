using System;

namespace FluentValidator
{
    public class ValidationRule : IValidationRule
    {
        public ValidationRule(string message, Func<object, bool> predicate)
        {
            Message = message;
            Predicate = predicate;
        }

        public ValidationRule(Func<object, bool> pred)
        {
            Predicate = pred;
        }

        public void WithMessage(string message)
        {
            Message = message;
        }

        public void WithMessage(string format, params object[] args)
        {
            Message = string.Format(format, args);
        }

        public string Message { get; private set; }

        public Func<object,bool> Predicate { get; set; } 
    }
}