namespace FluentValidator.Tests
{
    public class SampleValidator : BaseValidator<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).IsNotEmpty();
            RuleFor(x => x.DateOfBirth).IsNotNull();

            return true;
        }
    }
}