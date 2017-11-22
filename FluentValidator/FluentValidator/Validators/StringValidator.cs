using System;
using System.Text.RegularExpressions;

namespace FluentValidator.Validators
{
    public class StringValidator<TEntity> : PropertyValidator, IStringValidatorOptions<TEntity> where TEntity : class
    {
        public StringValidator(Func<object, string> getter, string fieldName)
            : base(fieldName)
        {
            Getter = getter;
        }

        public IStringValidatorOptions<TEntity> NotEmpty()
        {
            AddRule<string>(string.IsNullOrWhiteSpace).WithMessage("The property {0} was empty", FieldName);

            return this;
        }

        public IStringValidatorOptions<TEntity> DependentRule(Func<TEntity, bool> dependRule)
        {
            AddDendentRule(dependRule);

            return this;
        }

        public IStringValidatorOptions<TEntity> Matches(Regex regex)
        {
            AddRule<string>(x => !regex.IsMatch(x));

            return this;
        }

        public IStringValidatorOptions<TEntity> LengthEqualsTo(int lenght)
        {
            AddRule<string>(x => x.Length != lenght);
            return this;
        }

        public IStringValidatorOptions<TEntity> LengthLessThan(int maxLength)
        {
            AddRule<string>(x => !(x != null && x.Length <= maxLength));

            return this;
        }

        public IStringValidatorOptions<TEntity> NullOrLengthLessThan(int maxLength)
        {
            AddRule<string>(x => !(x == null || x.Length <= maxLength));

            return this;
        }

        public IStringValidatorOptions<TEntity> MustBe(Func<string, bool> pred)
        {
            AddRule<string>(q => !pred(q));

            return this;
        }

        public IStringValidatorOptions<TEntity>  WithMessage(string message)
        {
            return WithMessageInt<StringValidator<TEntity>>(message);
        }

        public IStringValidatorOptions<TEntity> WithName(string overiddenName)
        {
            return WithNameInt<StringValidator<TEntity>>(overiddenName);
        }

        public IStringValidatorOptions<TEntity> StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<StringValidator<TEntity>>();
        }

        public IStringValidatorOptions<TEntity> When(Func<TEntity, bool> predicate)
        {

            return WhenInt<StringValidator<TEntity>>(o => predicate((TEntity)o));
        }
    }
}