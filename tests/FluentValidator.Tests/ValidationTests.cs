﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentValidator.Tests.Samples;
using NUnit.Framework;

namespace FluentValidator.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Validate_StringPropertyEmpty_ValidationError()
        {
            var validator = new TestValidator();


            var validationResult = validator.Validate(
                new CreateEmployeeRequest
                {
                    FirstName = "",
                    EmployeeID = 4
                });

            var validationFailure = validationResult.ValidationFailures.First();
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("FirstName"));

            Assert.That(validationFailure.ValidationMessages.First(), Is.EqualTo("The property FirstName was empty"));
        }

        [Test]
        public void Validate_PropertyHasManyErrors_ValidationError()
        {
            var validator = new TestValidatorWithManyErrors();

            var validationResult = validator.Validate(
                new CreateEmployeeRequest
                {
                    FirstName = "asdf",
                    EmployeeID = 2,
                    DateOfBirth = DateTime.Now.AddYears(1)

                });

            var validationFailures = validationResult.ValidationFailures.ToList();
            var validationFailure = validationFailures.First();
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));

            var validationMessages = validationFailure.ValidationMessages.ToList();
            Assert.That(validationFailures.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Contains("Less error"),Is.True);
            Assert.That(validationMessages.Contains("Greater error"),Is.True);
        }

        [Test]
        public void Validate_PropertyHasManyErrorsAndStopOnFirstFailure_OnlyOneValidationError()
        {
            var validator = new TestValidatorWithStopOnFirstFailure();

            var validationResult = validator.Validate(
                new CreateEmployeeRequest
                {
                    EmployeeID = 2,

                });

            var validationFailures = validationResult.ValidationFailures.ToList();
            var validationFailure = validationFailures.First();

            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailures.Count, Is.EqualTo(1));
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));

            var validationMessages = validationFailure.ValidationMessages.ToList();
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.First(), Is.EqualTo("Less error"));
        }


        [Test]
        public void Validate_LessValidator_DefaultValidationMessage()
        {
            var validator = new TestValidator4();

            var validationResult = validator.Validate(
                new CreateEmployeeRequest
                {
                    EmployeeID = -1,
                });

            var validationFailures = validationResult.ValidationFailures.ToList();
            var validationFailure = validationFailures.First();

            var validationMessages = validationFailure.ValidationMessages.ToList();

            Assert.That(validationMessages.Count, Is.EqualTo(1));

            Assert.That(validationMessages, Does.Contain("The value of EmployeeID must be greater than 0"));
        }

        [Test]
        public void Validate_GreatedValidator_DefaultValidationMessage()
        {
            var validator = new TestValidator4();

            var validationResult = validator.Validate(
                new CreateEmployeeRequest
                {
                    EmployeeID = 4,
                });

            var validationFailures = validationResult.ValidationFailures.ToList();
            var validationFailure = validationFailures.First();

            var validationMessages = validationFailure.ValidationMessages.ToList();

            Assert.That(validationMessages.Count, Is.EqualTo(1));

            Assert.That(validationMessages, Does.Contain("The value of EmployeeID must be less than 3"));
        }


        [Test]
        public void Validate_TwoTimes_NotAccumulateErrorsValidationError()
        {
            var validator = new TestValidator2();


            var first = new CreateEmployeeRequest
                        {
                            FirstName = "",
                            EmployeeID = 4
                        };
            var second = new CreateEmployeeRequest
                         {
                             FirstName = "asdf",
                             EmployeeID = 2
                         };
            validator.Validate(first);
            var validationResult = validator.Validate(second);

            Assert.That(validationResult.IsValid, Is.False);
            var validationFailures = validationResult.ValidationFailures.ToList();
            Assert.That(validationFailures.Count, Is.EqualTo(1));
            Assert.That(validationFailures[0].ValidationMessages.Count(), Is.EqualTo(1));
            Assert.That(
                validationFailures[0].ValidationMessages.First(),
                Is.EqualTo("The value of EmployeeID must be greater than 3"));
        }


        [Test]
        public void Validate_DateTimePropertyEmpty_ValidationError()
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
            Assert.That(
                validationFailure.ValidationMessages.First(),
                Is.EqualTo("The property DateOfBirth must be less than today"));
        }

        [Test]
        public void Validate_IntPropertyEmpty_SetDefaultValidationMessage()
        {
            var validator = new TestValidator2();

            var entity = new CreateEmployeeRequest
                         {
                             FirstName = "asdf",
                             EmployeeID = 1,
                             DateOfBirth = DateTime.Now.AddMonths(1)
                         };

            var validationResult = validator.Validate(entity);
            var validationFailure = validationResult.ValidationFailures.First();
            Assert.That(validationFailure.FieldName, Is.EqualTo("EmployeeID"));
            Assert.That(
                validationFailure.ValidationMessages.First(),
                Is.EqualTo("The value of EmployeeID must be greater than 3"));
        }

        [Test]
        public void Validate_Performance()
        {
            var validator = new TestValidator();

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
            Assert.That(validationFailure.ValidationMessages.First(), Is.EqualTo("Message"));
        }


        [Test]
        public void When_FirstNameShouldBeNotEmptyWhenIdGreateThenZero()
        {
            var validator = new TestValidatorWithWhen();

            var em = new CreateEmployeeRequest
            {
                Id = 2,
                FirstName = "",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validationResult = validator.Validate(em);

            Assert.That(validationResult.IsValid, Is.False);
        }

        [Test]
        public void When_FirstNameRuleNotApplyWhenIdLessThenZero()
        {
            var validator = new TestValidatorWithWhen();

            var em = new CreateEmployeeRequest
            {
                Id = -1,
                FirstName = "",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validationResult = validator.Validate(em);

            Assert.That(validationResult.IsValid, Is.True);
        }

        [Test]
        public void WhenForDependentRule_FirstNameShouldBeEqualToLastName_IfIdGreateThenZero()
        {
            var validator = new TestValidatorWithWhenForDependendRule();

            var em = new CreateEmployeeRequest
            {
                Id = 2,
                FirstName = "a",
                LastName = "a",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validationResult = validator.Validate(em);

            var message = validationResult.ValidationFailures.ToArray()[0].ValidationMessages.First();
           
            Assert.That(validationResult.IsValid, Is.False);

            Assert.That(message, Is.EqualTo("Depend Rule not passed"));
        }

        [Test]
        public void WhenForDependentRule_DependRuleNotAppliedWhenIdLessThenZero()
        {
            var validator = new TestValidatorWithWhenForDependendRule();

            var em = new CreateEmployeeRequest
            {
                Id = -1,
                FirstName = "a",
                LastName = "a",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)
            };

            var validationResult = validator.Validate(em);
            var enumerable = validationResult.ValidationFailures.Select(x=> string.Format(x.FieldName +" - " + string.Join(",",x.ValidationMessages)));
            Console.WriteLine(string.Join("," ,enumerable ));
            Assert.That(validationResult.IsValid, Is.True);
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

        [Test]
        public void DependantRule_ValidatesAndSetsMessage()
        {
            var validator = new DependentRuleTestValidator();

            var em = new CreateEmployeeRequest
            {
                Id = -10,
                FirstName = "qwer",
                EmployeeID = 1,
                DateOfBirth = DateTime.Now.AddMonths(-1)

            };

            var validationResult = validator.Validate(em);

            var validationFailure = validationResult.ValidationFailures.ToList()[0];

            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("FirstName"));
            Assert.That(validationFailure.ValidationMessages.ToList()[0], Is.EqualTo("Id should be more than zero"));

        }



        [Test]
        public void NullableProperty_ValidatesAndSetsMessage()
        {
            var validator = new TestValidatorForNullableProperties();

            var em = new CreateEmployeeRequest
            {
               NullableNumber = 2

            };

            var validationResult = validator.Validate(em);

            var validationFailure = validationResult.ValidationFailures.ToList()[0];

            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationFailure.FieldName, Is.EqualTo("NullableNumber"));
            Assert.That(validationFailure.ValidationMessages.ToList()[0], Is.EqualTo("The value of NullableNumber must be greater than or equal to 3"));

        }

        [Test]
        public void NullableProperty_ValueEquals_ValidatesAndSetsMessage()
        {
            var validator = new TestValidatorForNullableProperties();

            var em = new CreateEmployeeRequest
            {
                NullableNumber = 3

            };

            var validationResult = validator.Validate(em);


            Assert.That(validationResult.IsValid, Is.True);

        }

        [Test]
        public void NullableProperty_ValueIsNull_ValidatesAndSetsMessage()
        {
            var validator = new TestValidatorForNullableProperties();

            var em = new CreateEmployeeRequest
            {
                NullableNumber = null

            };

            var validationResult = validator.Validate(em);


            Assert.That(validationResult.IsValid, Is.False);
            var validationFailure = validationResult.ValidationFailures.ToList()[0];
            Assert.That(validationFailure.ValidationMessages.First(),Is.EqualTo("The value of NullableNumber must not be null"));
        }
    }
}