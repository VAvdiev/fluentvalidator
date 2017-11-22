namespace FluentValidator.Tests
{
    public class TestValidator4 : BaseValidator<CreateEmployeeRequest>
    {
        public TestValidator4()
        {
            RuleFor(x => x.EmployeeID).StopOnFirstFailure()
                                      .GreaterThan(0)
                                      .LessThan(3);
        }
    }
}