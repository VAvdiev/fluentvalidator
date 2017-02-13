using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidator.Validators;

namespace FluentValidator
{
    public abstract class  BaseValidator<TEntity> where TEntity:class 
    {
        readonly IList<IValidatorResult> _validatorResults = new List<IValidatorResult>();

        public IEnumerable<IValidatorResult> Violations()
        {
            return _validatorResults.Where(x => !x.IsValid).ToList();
        } 

        public virtual void Reset()
        {
            _validatorResults.Clear();
        }

        public int ViolationsCount()
        {
            return  _validatorResults.Count(x => !x.IsValid);
        }

        protected IntValidator RuleFor(Expression<Func<TEntity, int>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new IntValidator(o=>getter((TEntity)o), propertyName);

            _validatorResults.Add(intValidator);

            return intValidator;
        }


        IValidatorResult Create<TValidator>(Expression<Func<TEntity, int>> getterExpression) where TValidator:IValidatorResult
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

            _validatorResults.Add(intValidator);

            return intValidator;
        }


        protected DateTimeValidator RuleFor(Expression<Func<TEntity, DateTime>> getterExpression)
        {
            var getter = PropertyExpressionHelper.InitializeGetter(getterExpression);
            var propertyName = PropertyExpressionHelper.GetPropertyName(getterExpression);

            var intValidator = new DateTimeValidator(o => getter((TEntity)o), propertyName);

            _validatorResults.Add(intValidator);

            return intValidator;
        }

        public IEnumerable<IValidatorResult> Validate(TEntity entity)
        {
            foreach (var validatorResult in _validatorResults)
            {
                validatorResult.Validate(entity);
            }
            return _validatorResults.Where(x => !x.IsValid);
        }
    }
}