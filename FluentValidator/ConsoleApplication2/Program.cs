using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentValidator;
using FluentValidator.Tests;

namespace ConsoleApplication2 
{
    class Program
    {

        static void Main(string[] args)
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
            Console.WriteLine("My Elapsed: " + stopwatch.ElapsedMilliseconds);

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            FluentValidator(empList);

            stopwatch2.Stop();

            Console.WriteLine("Elapsed: " + stopwatch2.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static void MyValidator(List<CreateEmployeeRequest> empList)
        {
            var validator = new TestValidator();

            validator.Configure();
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
