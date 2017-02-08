using NUnit.Framework;

namespace FluentValidator.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Validate()
        {
            var validator = new SampleValidator();

            validator.Configure();

            validator.Validate(new CreateEmployeeRequest {FirstName = "",
                EmployeeID = 4});

            Assert.That(validator.ViolationsCount(),Is.EqualTo(1));
        }
    }
}
