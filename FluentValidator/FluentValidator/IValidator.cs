using System.Collections.Generic;

namespace FluentValidator
{
    public interface IValidator
    {
         bool IsValid { get; }
        string ValidationMessage { get; }

        IEnumerable<string> ValidationFailures { get; }
        string FieldName { get; }
        void Validate(object entity);
        void Reset();
    }
}
