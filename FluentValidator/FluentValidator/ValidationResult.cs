using System.Collections.Generic;
using System.Linq;

namespace FluentValidator
{
    public class ValidationResult
    {
        public ValidationResult(IEnumerable<ValidationFailure> validationFailures)
        {
            ValidationFailures = validationFailures;
        }

        public bool IsValid
        {
            get { return !ValidationFailures.Any(); }
        }

        public IEnumerable<ValidationFailure> ValidationFailures { get; private set; }
    }
}