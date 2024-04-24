namespace FluentValidator.Tests.Samples
{
    public class TestValidator : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}