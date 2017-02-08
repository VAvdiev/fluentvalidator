using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentValidator
{
    public abstract class  BaseValidation<TEntity> where TEntity:class 
    {
        readonly IList<IValidatorResult> _validatorResults = new List<IValidatorResult>();

        public virtual BaseValidation<TEntity> Validate()
        {
            return this;
        }

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
            var getter = getterExpression.Compile();

            var intValidator = new IntValidator(getter(null),"");

            _validatorResults.Add(intValidator);

            return intValidator;
        }

        protected StringValidator RuleFor(Expression<Func<TEntity, string>> getterExpression)
        {
            var getter = getterExpression.Compile();

            var intValidator = new StringValidator(getter(null), "");

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
        protected DateTimeValidator RuleFor(Expression<Func<TEntity, DateTime>> getterExpression)
        {
            var getter = getterExpression.Compile();

            var intValidator = new DateTimeValidator(getter(null), "");

            _validatorResults.Add(intValidator);

            return intValidator;
        }
    }
}