namespace FluentValidator.Tests.Samples
{
    public class TestValidatorWithWhen : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithWhen()
        {
            RuleFor(x => x.FirstName).NotEmpty().When(x => x.Id > 0);
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}    }
}