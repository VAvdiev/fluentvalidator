namespace FluentValidator.Tests
{
    public class TestValidator4 : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator4()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).LessThan(0);
            RuleFor(x => x.FirstName).IsNotEmpty().WithMessage("Message");
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();

        }
    }
}