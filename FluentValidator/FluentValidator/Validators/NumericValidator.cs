using System;

namespace FluentValidator.Validators
{
    public class NumericValidator<TEntity,TProperty> : PropertyValidator where TProperty : IComparable where TEntity: class 
    {
        public NumericValidator(Func<object, TProperty> getter, string fieldName)
            : base(o => getter(o), fieldName)
        {
        }

        public NumericValidator<TEntity,TProperty> GreaterThan(TProperty value)
        {
            AddRule<TProperty>(x => x.CompareTo(value) <= 0)
                .WithMessage("The value of {0} must be greater than " + value, FieldName);
            return this;
        }

        public NumericValidator<TEntity,TProperty> GreaterThanOrEqualTo(TProperty value)
        {
            AddRule<TProperty>(x => !(x.CompareTo(value) > 0 || x.CompareTo(value) == 0))
                .WithMessage("The value of {0} must be greater than " + value, FieldName);
            return this;
        }

        public NumericValidator<TEntity,TProperty> LessThan(TProperty val)
        {
            AddRule<TProperty>(x => x.CompareTo(val) >= 0)
                .WithMessage("The value of {0} must be less than " + val, FieldName);
            return this;
        }

        public NumericValidator<TEntity, TProperty> LessThanOrEqualTo(TProperty value)
        {
            AddRule<TProperty>(x => x.CompareTo(value) >= 0)
                .WithMessage("The value of {0} must be less than or equal to" + value, FieldName);
            return this;
        }

        public NumericValidator<TEntity,TProperty> MustBe(Func<TProperty, bool> pred)
        {
            AddRule<TProperty>(q => !pred(q));
            return this;
        }

        public NumericValidator<TEntity,TProperty> WithMessage(string message)
        {
            return WithMessageInt<NumericValidator<TEntity,TProperty>>(message);
        }

        public NumericValidator<TEntity, TProperty> When(Func<TEntity, bool> predicate)
        {
            return WhenInt<NumericValidator<TEntity, TProperty>>(o => predicate((TEntity)o));
        }

        public NumericValidator<TEntity,TProperty> StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<NumericValidator<TEntity,TProperty>>();
        }

        public NumericValidator<TEntity,TProperty> DependentRule(Func<TEntity,bool> dependentRule)
        {
            AddDendentRule(dependentRule);
            return this;
        }

        public NumericValidator<TEntity, TProperty> WithName(string overiddenName)
        {
            return WithNameInt<NumericValidator<TEntity, TProperty>>(overiddenName);

        }

    }
}