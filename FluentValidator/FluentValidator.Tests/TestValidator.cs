namespace FluentValidator.Tests
{
    public class TestValidator : BaseValidation<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday();

            return true;
        }
    }
}