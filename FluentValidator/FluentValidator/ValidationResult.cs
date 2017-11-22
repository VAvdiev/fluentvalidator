using System.Collections.Generic;
using System.Linq;

namespace FluentValidator
{
    public class ValidationResult
    {
        /// <summary>
        /// For serialization
        /// </summary>
        public ValidationResult()
        {
            ValidationFailures = new List<ValidationFailure>();
        }

        public ValidationResult(IEnumerable<ValidationFailure> validationFailures):this()
        {
            ValidationFailures = validationFailures;
        }

        public bool IsValid { get { return !ValidationFailures.Any(); } }

        public IEnumerable<ValidationFailure> ValidationFailures { get; private set; }
    }
}