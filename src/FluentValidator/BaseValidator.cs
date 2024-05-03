using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidator.Validators;

namespace FluentValidator
{
    public abstract class BaseValidator<TEntity> : IValidator<TEntity> where TEntity : class 
    {
        private readonly IList<IPropertyValidator> _validators = new List<IPropertyValidator>();

        protected NumericValidator<TEntity,T> RuleFor<T>(Expression<Func<TEntity, T>> getterExpression) where T : struct , IComparable
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new NumericValidator<TEntity,T>(o => getter((TEntity)o), propertyName);

            _validators.Add(intValidator);

            return intValidator;
        }

        protected NullableNumericValidator<TEntity, TProperty> RuleFor<TProperty>(Expression<Func<TEntity, TProperty?>> getterExpression) where TProperty : struct, IComparable
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new NullableNumericValidator<TEntity, TProperty>(o => getter((TEntity)o), propertyName);

            _validators.Add(intValidator);

            return intValidator;
        }

        protected StringValidator<TEntity> RuleFor(Expression<Func<TEntity, string>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var validator = new StringValidator<TEntity>(o => getter((TEntity)o), propertyName);

            _validators.Add(validator);

            return validator;
        }


        protected DateTimeValidator RuleFor(Expression<Func<TEntity, DateTime>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new DateTimeValidator(o => getter((TEntity)o), propertyName);

            _validators.Add(intValidator);

            return intValidator;
        }


        public ValidationResult Validate(TEntity entity)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(entity);
            }
            var validationFailures = _validators.Where(x => !x.IsValid)
                .Select(validator => new ValidationFailure(validator.FieldName, validator.ValidationFailures.ToArray()))
                .ToList();
            AdditionalValidate(entity, validationFailures);
            return new ValidationResult(validationFailures);
        }

        protected virtual void AdditionalValidate(TEntity entity, ICollection<ValidationFailure> validationFailures)
        {
        }
    }
}