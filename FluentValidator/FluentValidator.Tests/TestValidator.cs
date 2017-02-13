namespace FluentValidator.Tests
{
    public class TestValidator : BaseValidator<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();

            return true;
        }
    }
}