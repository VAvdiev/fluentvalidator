using System;
using System.Text.RegularExpressions;

namespace FluentValidator.Validators
{
    public interface IStringValidatorOptions<out TEntity> where TEntity : class
    {
        IStringValidatorOptions<TEntity> NotEmpty();

        IStringValidatorOptions<TEntity> WithMessage(string message);
        IStringValidatorOptions<TEntity> StopOnFirstFailure();
        IStringValidatorOptions<TEntity> When(Func<TEntity, bool> predicate);

        IStringValidatorOptions<TEntity> DependentRule(Func<TEntity, bool> dependRule);
        IStringValidatorOptions<TEntity> Matches(Regex regex);
        IStringValidatorOptions<TEntity> MustBe(Func<string, bool> pred);
        IStringValidatorOptions<TEntity> LengthEqualsTo(int lenght);
        IStringValidatorOptions<TEntity> LengthLessThan(int maxLength);
        IStringValidatorOptions<TEntity> NullOrLengthLessThan(int maxLength);
        IStringValidatorOptions<TEntity> WithName(string overiddenName);
    }
}