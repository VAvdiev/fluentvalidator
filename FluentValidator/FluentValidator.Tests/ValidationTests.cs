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

            validator.Configure();

            validator.Validate(new CreateEmployeeRequest {FirstName = "", EmployeeID = 4});

            Assert.That(validator.ViolationsCount(),Is.EqualTo(1));
            var validatorResults = validator.Violations().ToList();
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The property FirstName was empty"));
        }

        [Test]
        public void Validate_TwoTimes_NotAccumulateErrorsValitationError()
        {
            var validator = new TestValidator();

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
            Assert.That(validatorResults[0].ValidationMessage, Is.EqualTo("The value of EmployeeID must be greater than 3"));
        }

        [Test]
        public void Validate_Performance()
        {
            var validator = new TestValidator();

            validator.Configure();

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

    }
}
