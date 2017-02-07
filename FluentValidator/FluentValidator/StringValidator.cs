namespace FluentValidator
{
    class StringValidator : BaseValidator
    {
        public StringValidator(string value,string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public StringValidator IsNotEmpty()
        {
            if (string.IsNullOrEmpty(Value))
            {
                SetFailure("String was empty");
            }
            return this;
        }
        public string Value { get; set; }

    }
}