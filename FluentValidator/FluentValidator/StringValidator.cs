using System;

namespace FluentValidator
{
    public class StringValidator : BaseValidator
    {
        public StringValidator(string value,string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public StringValidator(Func<object, string> getter, string fieldName) : base(fieldName)
        {
            Getter = getter;
        }

        public StringValidator IsNotEmpty()
        {
            AddRule<string>(string.IsNullOrEmpty).WithMessage("The property {0} was empty", FieldName);

            return this;
        }
        public string Value { get; set; }

    }
}