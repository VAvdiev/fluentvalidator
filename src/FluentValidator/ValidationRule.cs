using System;

namespace FluentValidator
{
    public class ValidationRule : IValidationRule
    {
        public ValidationRule(Func<object, bool> pred)
        {
            RulePredicate = pred;
            Predicate = o => true;
        }

        public void WithMessage(string message)
        {
            Message = message;
        }

        public void WithMessage(string format, params object[] args)
        {
            Message = string.Format(format, args);
        }

        public void WhenPredicate(Func<object, bool> predicate)
        {
            Predicate = predicate;
        }
    

        public string Message { get; private set; }

        public Func<object,bool> RulePredicate { get; set; } 
        public Func<object,bool> Predicate { get; private set; } 
    }
}