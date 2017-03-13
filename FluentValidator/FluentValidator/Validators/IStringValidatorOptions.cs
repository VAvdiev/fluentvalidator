namespace FluentValidator.Validators
{
    public interface IStringValidatorOptions 
    {
        IStringValidatorOptions NotEmpty();

        IStringValidatorOptions WithMessage(string message);
        IStringValidatorOptions StopOnFirstFailure();
    }
}