using System;

namespace FluentValidator.Tests
{
    
    public class CreateEmployeeRequest
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int LocationID { get; set; }

        public int? NullableNumber { get; set; }
    }
}