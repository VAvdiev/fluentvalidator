using System;

namespace FluentValidator
{
    public class DateTimeValidator: BaseValidator
    {
        public DateTimeValidator(Func<object, DateTime> getter, string fieldName) : base(fieldName)
        {
            Getter = o => getter(o);
        }

        public DateTimeValidator IsNotNull()
        {
            AddRule<DateTime?>( x => !x.HasValue).WithMessage("The property {0} Value is null", FieldName);

            return this;
        }

        public DateTimeValidator MoreThanToday()
        {
            AddRule<DateTime>(x => x < DateTime.Now).WithMessage("The property {0} must be more than {1}", FieldName, DateTime.Now);
            return this;
        }

        public DateTimeValidator LessThanToday()
        {
            AddRule<DateTime>(x => x >= DateTime.Now).WithMessage("The property {0} must be less than today", FieldName);
            return this;
        }
    }
}