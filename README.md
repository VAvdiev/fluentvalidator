# High performance lightweight validation framework using fluent API 
### What is FastValidator?

FastValidator is a simple lightwight library for validation of business rules against object.
It uses fluent API and declarative style to speceify rules

This is the main repository for AutoMapper, but there's more:

### How do I get started?

First, create a validator class derived from BaseValidator<>: and specify the validation rules

```csharp
    public class CreateEmployeeRequestValidator : BaseValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.EmployeeID).GreaterThan(3).WithMessage("Message");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanToday().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
```

Then in your application code, execute:

```csharp
    var validator = new CreateEmployeeRequestValidator();

    var request = new CreateEmployeeRequest
    {
        FirstName = "asdf",
        EmployeeID = 1,
        DateOfBirth = dateOfBirth
    };

    validator.Validate(request);
```