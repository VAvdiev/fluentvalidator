using System;

namespace FluentValidator.Validators
{
    public interface IStringValidatorOptions<out TEntity> where TEntity: class 
    {
        IStringValidatorOptions<TEntity> NotEmpty();

        IStringValidatorOptions<TEntity> WithMessage(string message);
        IStringValidatorOptions<TEntity> StopOnFirstFailure();
        IStringValidatorOptions<TEntity> When(Func<TEntity,bool> predicate);
    }
}