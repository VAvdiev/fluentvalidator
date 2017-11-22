using System.Collections.Generic;
using System.Linq;

namespace FluentValidator
{
    public class ValidationFailure
    {
        public ValidationFailure(string fieldName, IEnumerable<string> validationMessages)
        {
            FieldName = fieldName;
            ValidationMessages = validationMessages;
        }

        public string FieldName { get; private set; }

        public IEnumerable<string> ValidationMessages { get; private set; } 
    }
}