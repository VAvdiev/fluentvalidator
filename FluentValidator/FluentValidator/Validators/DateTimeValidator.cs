using System;

namespace FluentValidator.Validators
{
    public class DateTimeValidator: PropertyValidator
    {
        public DateTimeValidator(Func<object, DateTime> getter, string fieldName) : base(fieldName)
        {
            Getter = o => getter(o);
        }

        public DateTimeValidator NotNull()
        {
            AddRule<DateTime?>(x => !x.HasValue).WithMessage("The property {0} Value is null", FieldName);

            return this;
        }

        public DateTimeValidator MoreThanToday()
        {
            AddRule<DateTime>(x => x < DateTime.Now).WithMessage("The property {0} must be more than {1}", FieldName, DateTime.Now);
            return this;
        }


        public DateTimeValidator GreaterThanOrEqualTo(DateTime value)
        {
            AddRule<DateTime>(x => x < value).WithMessage("The property {0} must be more than {1}", FieldName, value);
            return this;
        }


        public DateTimeValidator LessThanToday()
        {
            AddRule<DateTime>(x => x >= DateTime.Now).WithMessage("The property {0} must be less than today", FieldName);
            return this;
        }

        public DateTimeValidator MustBe(Func<DateTime, bool> pred)
        {
            AddRule<DateTime>(q => !pred(q));
            return this;
        }

        public DateTimeValidator WithMessage(string message)
        {
            return WithMessageInt<DateTimeValidator>(message);
        }

        public DateTimeValidator StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<DateTimeValidator>();
        }
    }
}