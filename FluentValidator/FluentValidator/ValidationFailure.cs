using System.Collections.Generic;

namespace FluentValidator
{
    public class ValidationFailure
    {
        public ValidationFailure(string fieldName, IEnumerable<string> validationMessages)
        {
            FieldName = fieldName;
            ValidationMessages = validationMessages;
        }

        public string FieldName { get; }

        public IEnumerable<string> ValidationMessages { get; private set; } 
    }
}