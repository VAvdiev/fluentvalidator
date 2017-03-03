namespace FluentValidator.Tests
{
    public class TestValidator2 : BaseValidator<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).IsNotEmpty().WithMessage("Message");
            RuleFor(x => x.DateOfBirth).LessThanToday().IsNotNull();
            RuleFor(x => x.DateOfBirth).IsNotNull();

            return true;
        }
    }
}