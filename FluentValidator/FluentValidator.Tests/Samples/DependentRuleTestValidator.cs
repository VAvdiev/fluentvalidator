namespace FluentValidator.Tests.Samples
{
    public class DependentRuleTestValidator : BaseValidator<CreateEmployeeRequest>
    {
        public DependentRuleTestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .DependentRule(x => x.Id > 0)
                .WithMessage("Id should be more than zero");
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
        }
    }
}    }
}