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
      //  [Ignore("Run manualy")]
        public void PerformanceTests()
        {
            Random random = new Random();

            var empList = new List<CreateEmployeeRequest>();
            var dateOfBirth = DateTime.Now.AddMonths(-1);

            for (int i = 0; i < 1000000; i++)
            {
                var em = new CreateEmployeeRequest
                {
                    Id = 1,
                    FirstName = "asdf",
                    EmployeeID = 1,
                    DateOfBirth = dateOfBirth
                };
                empList.Add(em);
            }

           
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            MyValidator(empList);

            stopwatch2.Stop();

            Console.WriteLine("Elapsed: " + stopwatch2.ElapsedMilliseconds);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            FluentValidator(empList);

            stopwatch.Stop();
            Console.WriteLine("My Elapsed: " + stopwatch.ElapsedMilliseconds);

        }


        [Test]
        //  [Ignore("Run manualy")]
        public void PerformanceDependantRuleTests()
        {
            Random random = new Random();

            var empList = new List<CreateEmployeeRequest>();
            var dateOfBirth = DateTime.Now.AddMonths(-1);

            for (int i = 0; i < 1000000; i++)
            {
                var em = new CreateEmployeeRequest
                {
                    Id = 1,
                    FirstName = "asdf",
                    EmployeeID = 1,
                    DateOfBirth = dateOfBirth
                };
                empList.Add(em);
            }


            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            MyDepentValidator(empList);

            stopwatch2.Stop();

            Console.WriteLine("Elapsed: " + stopwatch2.ElapsedMilliseconds);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MyValidator(empList);

            stopwatch.Stop();
            Console.WriteLine("My Elapsed: " + stopwatch.ElapsedMilliseconds);

        }


        private static void MyValidator(List<CreateEmployeeRequest> empList)
        {
            var validator = new TestValidator();

            foreach (var request in empList)
            {
                validator.Validate(request);
            }
        }


        private static void MyDepentValidator(List<CreateEmployeeRequest> empList)
        {
            var validator = new DependentRuleTestValidator();

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