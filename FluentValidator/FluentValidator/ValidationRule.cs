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
        public string Message { get; private set; }

        public Func<object,bool> Predicate { get; set; } 
    }
}