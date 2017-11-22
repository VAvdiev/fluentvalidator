using System;

namespace FluentValidator.Validators
{
    internal class StringValidator<TEntity> : BaseValidator, IStringValidatorOptions<TEntity> where TEntity:class
    {
        public StringValidator(Func<object, string> getter, string fieldName) : base(fieldName)
        {
            Getter = getter;
        }

        public IStringValidatorOptions<TEntity> NotEmpty()
        {
            AddRule<string>(string.IsNullOrEmpty).WithMessage("The property {0} was empty", FieldName);

            return this;
        }

        public IStringValidatorOptions<TEntity> WithMessage(string message)
        {
            return WithMessageInt<StringValidator<TEntity>>(message);
        }

        public IStringValidatorOptions<TEntity> StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<StringValidator<TEntity>>();
        }

        public IStringValidatorOptions<TEntity> When(Func<TEntity, bool> predicate)
        {
            Func<object, bool> pred = o => predicate((TEntity) o);
            
            return WhenInt<StringValidator<TEntity>>(pred);
        }

        public IStringValidatorOptions<TEntity> DependentRule(Func<TEntity, bool> dependRule)
        {
            AddDendentRule(dependRule);

            return this;
        }

  
    }
}