namespace FluentValidator.Tests
{
    public class TestValidatorForNullableProperties : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorForNullableProperties()
        {
            //RuleFor(x => x.NullableNumber).GreaterThan(10).WithMessage("Message");
            RuleFor(x => x.NullableNumber).GreaterThanOrEqualTo(3);
        }
    }
}