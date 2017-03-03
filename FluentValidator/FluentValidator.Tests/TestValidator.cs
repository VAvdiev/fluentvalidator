namespace FluentValidator.Tests
{
    public class TestValidator : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();
        }
    }
}