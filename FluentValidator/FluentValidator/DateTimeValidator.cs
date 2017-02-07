using System;

namespace FluentValidator
{
    class DateTimeValidator: BaseValidator
    {
        private readonly DateTime? _value;

        public DateTimeValidator(DateTime value, string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public DateTimeValidator(DateTime? value, string fieldName) : base(fieldName)
        {
            Value = value;
        }

        public DateTimeValidator IsNotNull()
        {
            if (!Value.HasValue)
            {
                SetFailure("Value is null");
            }
            return this;
        }
        public DateTime? Value { get; private set; }
    }
}