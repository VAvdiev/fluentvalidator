namespace FluentValidator.Tests.Samples
{
    public class TestValidatorWithStopOnFirstFailure : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidatorWithStopOnFirstFailure()
        {
            RuleFor(x => x.EmployeeID)
                .StopOnFirstFailure()
                .LessThan(0)
                .WithMessage("Less error")
                .GreaterThan(3)
                .WithMessage("Greater error");
        }
    }
}    }
}