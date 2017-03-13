using System;

namespace FluentValidator.Validators
{
    internal class DateTimeValidator: BaseValidator, IDateTimeValidatorOptions
    {
        public DateTimeValidator(Func<object, DateTime> getter, string fieldName) : base(fieldName)
        {
            Getter = o => getter(o);
        }

        public IDateTimeValidatorOptions NotNull()
        {
            AddRule<DateTime?>( x => !x.HasValue).WithMessage("The property {0} Value is null", FieldName);

            return this;
        }

        public IDateTimeValidatorOptions MoreThanToday()
        {
            AddRule<DateTime>(x => x < DateTime.Today).WithMessage("The property {0} must be more than {1}", FieldName, DateTime.Now);
            return this;
        }

        public IDateTimeValidatorOptions LessThanToday()
        {
            AddRule<DateTime>(x => x >= DateTime.Today).WithMessage("The property {0} must be less than today", FieldName);
            return this;
        }

        public IDateTimeValidatorOptions WithMessage(string message)
        {
            return WithMessageInt<DateTimeValidator>(message);
        }

        public IDateTimeValidatorOptions StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<DateTimeValidator>();
        }
    }
}