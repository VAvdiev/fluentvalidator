namespace FluentValidator.Tests.Samples
{
    public class TestValidatorForNullableProperties : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorForNullableProperties()
        {
            RuleFor(x => x.NullableNumber)
                .NotNull()
                .GreaterThanOrEqualTo(3);
        }
    }
}