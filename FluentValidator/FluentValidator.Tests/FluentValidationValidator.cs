using System;
using FluentValidation;

namespace FluentValidator.Tests
{
    public class FluentValidationValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today).NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();

            return true;
        }
    }
}