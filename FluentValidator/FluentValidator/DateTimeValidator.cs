using System;

namespace FluentValidator
{
    public class DateTimeValidator: BaseValidator
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
            AddRule<DateTime?>( x => !x.HasValue).WithMessage("Value is null");

            if (!Value.HasValue)
            {
                SetFailure("Value is null");
            }
            return this;
        }
        public DateTime? Value { get; private set; }
    }
}