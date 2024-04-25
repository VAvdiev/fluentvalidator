namespace FluentValidator.Tests.Samples
{
    public class TestValidator2 : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator2()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Message not empty");
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
        }
    }
}    
