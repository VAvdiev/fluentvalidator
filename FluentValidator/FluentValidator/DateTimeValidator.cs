using System;

namespace FluentValidator
{
    public class DateTimeValidator: BaseValidator
    {
        private readonly DateTime? _value;
        private Func<object, DateTime> getter;

        public DateTimeValidator(Func<object, DateTime> getter, string fieldName) : base(fieldName)
        {
            Getter = o => getter(o);
        }

       

        public DateTimeValidator IsNotNull()
        {
            AddRule<DateTime?>( x => !x.HasValue).WithMessage("Value is null");

            if (!Value.HasValue)
            {
                //SetFailure("Value is null");
            }
            return this;
        }
        public DateTime? Value { get; private set; }
    }
}