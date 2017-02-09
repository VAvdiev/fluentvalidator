using System;
using System.Collections.Generic;

namespace FluentValidator
{
    public class IntValidator : BaseValidator
    {

        public IntValidator(Func<object, int> getter, string fieldName) : base(fieldName)
        {
            Getter = o=>getter(o);
        }

        public IntValidator GreaterThan(int val)
        {
            AddRule<int>(x => x < val)
                .WithMessage("The value of {0} should be greater than " + val, FieldName);
            return this;
        }

        public int? Value{ get; private set; }
    }
}