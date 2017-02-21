using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidator
{
    public interface IValidatorResult
    {
         bool IsValid { get; }
        string ValidationMessage { get; }
        string FieldName { get; }
        void Validate(object entity);
        void Reset();
    }
}
