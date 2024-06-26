﻿using System.Collections.Generic;

namespace FluentValidator;

internal interface IPropertyValidator
{
    bool IsValid { get; }

    IEnumerable<string> ValidationFailures { get; }
    string FieldName { get; }
    void Validate(object entity);
}