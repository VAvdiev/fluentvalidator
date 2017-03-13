using NUnit.Framework;

namespace FluentValidator.Tests
{

    [TestFixture]
    public class NumberValidatorTests
    {
        private TestNumericValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new TestNumericValidator();
        }
        [Test]
        public void Validate_GreateThanRule_IsInvalid()
        {
            var sampleDto = new SampleDto { NumericProperty = 2};

            var validationResult = _validator.Validate(sampleDto);

            Assert.That(validationResult.IsValid, Is.False);
        }

        [Test]
        public void Validate_GreateThanRule_IsInvalidIfEquals()
        {
            var validationResult = _validator.Validate(new SampleDto { NumericProperty = 5 });

            Assert.That(validationResult.IsValid, Is.False);
        }

        [Test]
        public void GreateThanRule_IfPropertyMoreThanRuleValue_IsValid()
        {
            var validationResult = _validator.Validate(new SampleDto { NumericProperty = 6 });

            Assert.That(validationResult.IsValid, Is.True);
        }

        [Test]
        public void LessThanRule_IsValid()
        {
            var validationResult = _validator.Validate(new SampleDto { Numeric2 = 2 });

            Assert.That(validationResult.IsValid, Is.True);
        }

        [Test]
        public void LessThanRule_IfEquals_IsInvalid()
        {
            var validationResult = _validator.Validate(new SampleDto { Numeric2 = 5 });

            Assert.That(validationResult.IsValid, Is.False);
        }

        [Test]
        public void LessThanRule_IfPropertyMoreThanRuleValue_IsInValid()
        {
            var validationResult = _validator.Validate(new SampleDto { Numeric2 = 6 });

            Assert.That(validationResult.IsValid, Is.False);
        }
    }

    class SampleDto
    {
        public SampleDto()
        {
            NumericProperty = 10;
            Numeric2 = 0;
        }
        public int NumericProperty { get; set; } 
        public int Numeric2 { get; set; } 
    }

    class TestNumericValidator: BaseValidator<SampleDto>
    {
        public TestNumericValidator()
        {
            RuleFor(x => x.NumericProperty).GreaterThan(5);
            RuleFor(x => x.Numeric2).LessThan(5);
        }
    }
}