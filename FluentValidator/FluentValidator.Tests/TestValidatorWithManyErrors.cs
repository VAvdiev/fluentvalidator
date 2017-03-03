namespace FluentValidator.Tests
{
    public class TestValidatorWithManyErrors : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithManyErrors()
        {
            RuleFor(x => x.EmployeeID).LessThan(0).WithMessage("Less error").GreaterThan(3).WithMessage("Greater error");
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();
        }
     
    }
}