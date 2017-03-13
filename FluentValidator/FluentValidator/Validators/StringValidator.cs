using System;

namespace FluentValidator.Validators
{
    internal class StringValidator : BaseValidator, IStringValidatorOptions
    {
        public StringValidator(Func<object, string> getter, string fieldName) : base(fieldName)
        {
            Getter = getter;
        }

        public IStringValidatorOptions NotEmpty()
        {
            AddRule<string>(string.IsNullOrEmpty).WithMessage("The property {0} was empty", FieldName);

            return this;
        }

        public IStringValidatorOptions WithMessage(string message)
        {
            return WithMessageInt<StringValidator>(message);
        }

        public IStringValidatorOptions StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<StringValidator>();
        }
    }
}