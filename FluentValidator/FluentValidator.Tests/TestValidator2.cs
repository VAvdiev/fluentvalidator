namespace FluentValidator.Tests
{
    public class TestValidator2 : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator2()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).IsNotEmpty().WithMessage("Message not empty");
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
        }
    }
}