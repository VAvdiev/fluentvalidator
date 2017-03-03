using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidator.Validators;

namespace FluentValidator
{
    public abstract class  BaseValidator<TEntity> where TEntity:class 
    {
        readonly IList<IValidator> _validators = new List<IValidator>();

        public IEnumerable<IValidator> Violations()
        {
            return _validators.Where(x => !x.IsValid).ToList();
        } 

        public virtual void Reset()
        {
            foreach (var validatorResult in _validators)
            {
                validatorResult.Reset();
            }
        }

        public int ViolationsCount()
        {
            return  _validators.Count(x => !x.IsValid);
        }

        protected IntValidator RuleFor(Expression<Func<TEntity, int>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new IntValidator(o=>getter((TEntity)o), propertyName);

            _validators.Add(intValidator);

            return intValidator;
        }


        IValidator Create<TValidator>(Expression<Func<TEntity, int>> getterExpression) where TValidator:IValidator
        {
            var result = (TValidator)typeof(TValidator)
                .GetConstructor(new[] { typeof(Expression<Func<TEntity, object>>) })
                .Invoke(new[] { getterExpression });
            return result;
        }

        protected StringValidator RuleFor(Expression<Func<TEntity, string>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new StringValidator(o => getter((TEntity)o), propertyName);

            _validators.Add(intValidator);

            return intValidator;
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
            Reset();
            foreach (var validatorResult in _validators)
            {
                validatorResult.Validate(entity);
            }
            var validationFailures = _validators.Where(x => !x.IsValid)
                .Select(validator => new ValidationFailure(validator.FieldName, validator.ValidationFailures))
                .ToList();

            return new ValidationResult(validationFailures);
        }
    }
}