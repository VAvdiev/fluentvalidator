namespace FluentValidator.Tests
{
    public class TestValidator4 : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator4()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).LessThan(0);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Message");
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();

        }
    }
}