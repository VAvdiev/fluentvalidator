using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var validator = new TestValidator();


            var validationResult = validator.Validate(new CreateEmployeeRequest {FirstName = "", EmployeeID = 4});

            var validationFailure = validationResult.ValidationFailures.First();
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("FirstName"));

            Assert.That(validationFailure.ValidationMessages.First(), Is.EqualTo("The property FirstName was empty"));
        }

        [Test]
        public void Validate_PropertyHasManyErrors_ValitationError()
        {
            var validator = new TestValidatorWithManyErrors();


            var validationResult = validator.Validate(new CreateEmployeeRequest { FirstName = "asdf", EmployeeID = 2 });

            var validationFailures = validationResult.ValidationFailures.ToList();
            var validationFailure = validationFailures.First();
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));

            var validationMessages = validationFailure.ValidationMessages.ToList();
            Assert.That(validationMessages.First(), Is.EqualTo("Less error"));
            Assert.That(validationMessages[1], Is.EqualTo("Greater error"));
        }


        [Test]
        public void Validate_TwoTimes_NotAccumulateErrorsValitationError()
        {
            var validator = new TestValidator2();

            validator.Configure();

            var first = new CreateEmployeeRequest { FirstName = "", EmployeeID = 4 };
            var second = new CreateEmployeeRequest { FirstName = "asdfas", EmployeeID = 2 };
            validator.Validate(first);
            validator.Validate(second);

            Assert.That(validator.ViolationsCount(), Is.EqualTo(1));
            var validatorResults = validator.Violations().ToList();
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The value of EmployeeID must be greater than 3"));
        }


        [Test]
        public void Validate_DateTimePropertyEmpty_ValitationError()
        {
            var validator = new TestValidator();


            var entity = new CreateEmployeeRequest
            {
                FirstName = "asdf",
                EmployeeID = 4,
                DateOfBirth = DateTime.Now.AddMonths(1)
            };

            var validationResult = validator.Validate(entity);
            var validationFailure = validationResult.ValidationFailures.First();
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("DateOfBirth"));
            Assert.That(validationFailure.ValidationMessages.First(), Is.EqualTo("The property DateOfBirth must be less than today"));
        }

        [Test]
        public void Validate_IntPropertyEmpty_SetDefaultValitationMessage()
        {
            var validator = new TestValidator2();

            validator.Configure();

            var entity = new CreateEmployeeRequest
            {
                FirstName = "asdf",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(1)
            };

            var validationResult = validator.Validate(entity);
            var validationFailure = validationResult.ValidationFailures.First();
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));
            Assert.That(validationFailure.ValidationMessages.First(), Is.EqualTo("The value of EmployeeID must be greater than 3"));
        }

        [Test]
        public void Validate_Performance()
        {
            var validator = new TestValidator();


            Random random = new Random();

            var empList = new List<CreateEmployeeRequest>();

            for (int i = 0; i < 1000000; i++)
            {
                var em = new CreateEmployeeRequest
                {
                    FirstName = "asdf",
                    EmployeeID = 1,
                    DateOfBirth = DateTime.Now.AddMonths(-1)
                };
                empList.Add(em);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var request in empList)
            {
                validator.Validate(request);
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds);
        }

   

        [Test]
        public void WithMessage_SetsValidationMessage()
        {
            var validator = new TestValidator();

            var em = new CreateEmployeeRequest
            {
                FirstName = "",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validatorResult = validator.Validate(em);
            var validationFailure = validatorResult.ValidationFailures.First();
            Assert.That(validatorResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));
            Assert.That(validationFailure.ValidationMessages.First(),Is.EqualTo("Message"));
        }

        [Test]
        public void TestName()
        {
            var validator = new FluentValidationValidator();
            validator.Configure();
            var em = new CreateEmployeeRequest
            {
                FirstName = "",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validationResult = validator.Validate(em);

            Assert.That(validationResult.IsValid, Is.False);
        }

    }
}
