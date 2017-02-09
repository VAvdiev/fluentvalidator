using System;
using System.Linq;
using NUnit.Framework;

namespace FluentValidator.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Validate_StringPropertyEmpty_ValitationError()
        {
            var validator = new SampleValidator();

            validator.Configure();

            validator.Validate(new CreateEmployeeRequest {FirstName = "", EmployeeID = 4});

            Assert.That(validator.ViolationsCount(),Is.EqualTo(1));
            var validatorResults = validator.Violations().ToList();
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The property FirstName was empty"));
        }

        [Test]
        public void Validate_DateTimePropertyEmpty_ValitationError()
        {
            var validator = new TestValidator();

            validator.Configure();

            validator.Validate(new CreateEmployeeRequest
            {
                FirstName = "asdf",
                EmployeeID = 4,
                DateOfBirth = DateTime.Now.AddMonths(1)
            });

            Assert.That(validator.ViolationsCount(), Is.EqualTo(1));
            var validatorResults = validator.Violations().ToList();
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The property DateOfBirth must be less than today"));
        }

        [Test]
        public void Validate_IntPropertyEmpty_ValitationError()
        {
            var validator = new TestValidator();

            validator.Configure();

            validator.Validate(new CreateEmployeeRequest
            {
                FirstName = "asdf",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            });

            Assert.That(validator.ViolationsCount(), Is.EqualTo(1));
            var validatorResults = validator.Violations().ToList();
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The property EmployeeID must be greater than 3"));
        }
    }
}
