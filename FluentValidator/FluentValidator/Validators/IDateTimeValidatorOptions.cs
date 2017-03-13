namespace FluentValidator.Validators
{
    public interface IDateTimeValidatorOptions 
    {
        IDateTimeValidatorOptions NotNull();
        IDateTimeValidatorOptions MoreThanToday();
        IDateTimeValidatorOptions LessThanToday();
        IDateTimeValidatorOptions WithMessage(string message);
        IDateTimeValidatorOptions StopOnFirstFailure();
    }
}