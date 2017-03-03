namespace FluentValidator.Tests
{
    public class TestValidatorWithManyErrors : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithManyErrors()
        {
            RuleFor(x => x.EmployeeID).LessThan(0).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();
        }
     
    }
}