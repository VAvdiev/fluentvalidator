using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace FluentValidator.Tests
{
    [TestFixture]
    public class ValidatorPerformanceTests
    {
        [Test]
        public void PerformanceTests()
        {
            Random random = new Random();

            var empList = new List<CreateEmployeeRequest>();
            var dateOfBirth = DateTime.Now.AddMonths(-1);

            for (int i = 0; i < 1000000; i++)
            {
                var em = new CreateEmployeeRequest
                {
                    FirstName = "asdf",
                    EmployeeID = 1,
                    DateOfBirth = dateOfBirth
                };
                empList.Add(em);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MyValidator(empList);

            stopwatch.Stop();
            Console.WriteLine($"FastValidator Elapsed: {stopwatch.ElapsedMilliseconds} ms");

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            FluentValidator(empList);

            stopwatch2.Stop();

            Console.WriteLine($"FluentValidation Elapsed: {stopwatch2.ElapsedMilliseconds} ms");
        }


        private static void MyValidator(List<CreateEmployeeRequest> empList)
        {
            var validator = new TestValidator();

            foreach (var request in empList)
            {
                validator.Validate(request);
            }
        }


        private static void FluentValidator(List<CreateEmployeeRequest> empList)
        {
            var validator = new FluentValidationValidator();

            validator.Configure();
            foreach (var request in empList)
            {
                validator.Validate(request);
            }
        }
    }


}