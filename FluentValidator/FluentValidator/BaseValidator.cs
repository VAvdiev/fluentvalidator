namespace FluentValidator
{
    class BaseValidator : IValidatorResult
    {
        public BaseValidator(string fieldName)
        {
            FieldName = fieldName;
            IsValid = true;
        }

        protected void SetFailure(string message)
        {
            ValidationMessage = message;
            IsValid = false;
        }
        public bool IsValid { get; protected set; }
        public string ValidationMessage { get; protected set; }
        public string FieldName { get; private set; }
    }
}