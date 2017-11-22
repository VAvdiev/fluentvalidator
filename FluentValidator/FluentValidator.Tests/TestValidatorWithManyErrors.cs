namespace FluentValidator.Tests
{
    public class TestValidatorWithManyErrors : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithManyErrors()
        {
            RuleFor(x => x.EmployeeID)
                .LessThan(0).WithMessage("Less error")
                .GreaterThan(3).WithMessage("Greater error");

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}