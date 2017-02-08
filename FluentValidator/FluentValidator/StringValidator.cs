namespace FluentValidator
{
    public class StringValidator : BaseValidator
    {
        public StringValidator(string value,string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public StringValidator IsNotEmpty()
        {
            AddRule<string>(string.IsNullOrEmpty).WithMessage("String was empty");

            if (string.IsNullOrEmpty(Value))
            {
                SetFailure("String was empty");
            }
            return this;
        }
        public string Value { get; set; }

    }
}