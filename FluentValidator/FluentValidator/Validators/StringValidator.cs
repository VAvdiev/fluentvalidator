using System;

namespace FluentValidator.Validators
{
    public class StringValidator : BaseValidator
    {
        public StringValidator(Func<object, string> getter, string fieldName) : base(fieldName)
        {
            Getter = getter;
        }

        public StringValidator IsNotEmpty()
        {
            AddRule<string>(string.IsNullOrEmpty).WithMessage("The property {0} was empty", FieldName);

            return this;
        }

        public StringValidator WithMessage(string message)
        {
            return WithMessageInt<StringValidator>(message);
        }
    }
}