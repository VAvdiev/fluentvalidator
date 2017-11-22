using System;
using System.Text.RegularExpressions;
using FluentValidation;

namespace FluentValidator.Tests
{
    public class FluentValidationValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public bool Configure()
        {
            //RuleFor(x => x.EmployeeID).GreaterThan(3).WithName("First").WithMessage("Bla bla");
            //RuleFor(x => x.FirstName).NotEmpty().WithName("First").WithMessage("Bla bla").Matches(new Regex(@"\W")).WithMessage("Not match regex");
            //RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today).NotNull();
            //RuleFor(x => x.DateOfBirth).NotNull();

            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today).NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();

            return true;
        }
    }
}