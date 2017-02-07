namespace FluentValidator
{
    class IntValidator : BaseValidator
    {
        public IntValidator(int value, string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public IntValidator(int? value, string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public IntValidator GreaterThan(int val)
        {
            if (Value< val)
            {
                SetFailure("The value should be greater than " + val);
            }
            return this;
        }
        public int? Value{ get; private set; }
    }
}