namespace FluentValidator.Tests.Samples
{
    public class TestValidatorWithWhenForDependendRule : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithWhenForDependendRule()
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .DependentRule(x => x.LastName != x.FirstName)
                .When(x => x.Id > 0)
                .WithMessage("Depend Rule not passed");
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}    
