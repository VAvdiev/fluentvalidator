using System;

namespace FluentValidator.Validators
{
   
    public class NullableNumericValidator<TEntity, TProperty> : PropertyValidator where TProperty: struct, IComparable where TEntity: class 
    {
        public NullableNumericValidator(Func<object, TProperty?> getter, string fieldName)
            : base(o => getter(o), fieldName)
        {
            
        }
        
        public NullableNumericValidator<TEntity, TProperty> NotNull()
        {
            AddRule<TProperty?>(x => x == null)
                .WithMessage("The value of {0} must be null ", FieldName);
            return this;
        }
        public NullableNumericValidator<TEntity,TProperty> GreaterThan(TProperty value)
        {
            AddRule<TProperty?>(x => !x.HasValue || x.Value.CompareTo(value) <= 0)
                .WithMessage("The value of {0} must be greater than " + value, FieldName);
            return this;
        }

        public NullableNumericValidator<TEntity,TProperty> GreaterThanOrEqualTo(TProperty value)
        {
            AddRule<TProperty?>(x => !x.HasValue || !(x.Value.CompareTo(value) > 0 || x.Value.CompareTo(value) == 0))
                .WithMessage("The value of {0} must be greater than or equal to " + value, FieldName);
            return this;
        }

        public NullableNumericValidator<TEntity,TProperty> LessThan(TProperty val)
        {
            AddRule<TProperty?>(x => !x.HasValue || x.Value.CompareTo(val) >= 0)
                .WithMessage("The value of {0} must be less than " + val, FieldName);
            return this;
        }

        public NullableNumericValidator<TEntity, TProperty> LessThanOrEqualTo(TProperty value)
        {
            AddRule<TProperty?>(x => !x.HasValue || x.Value.CompareTo(value) >= 0)
                .WithMessage("The value of {0} must be less than or equal to" + value, FieldName);
            return this;
        }

        public NullableNumericValidator<TEntity,TProperty> MustBe(Func<TProperty?, bool> predicate)
        {
            AddRule<TProperty?>(q => !predicate(q));
            return this;
        }

        public NullableNumericValidator<TEntity,TProperty> WithMessage(string message)
        {
            return WithMessageInt<NullableNumericValidator<TEntity, TProperty>>(message);
        }

        public NullableNumericValidator<TEntity, TProperty> WithName(string overridenName)
        {
            return WithNameInt<NullableNumericValidator<TEntity, TProperty>>(overridenName);
        }

        public NullableNumericValidator<TEntity, TProperty> When(Func<TEntity, bool> predicate)
        {
            return WhenInt<NullableNumericValidator<TEntity, TProperty>>(o => predicate((TEntity)o));
        }

        public NullableNumericValidator<TEntity,TProperty> StopOnFirstFailure()
        {
            return StopOnFirstFailureInt<NullableNumericValidator<TEntity, TProperty>>();
        }

        public NullableNumericValidator<TEntity,TProperty> DependentRule(Func<TEntity,bool> dependentRule)
        {
            AddDendentRule(dependentRule);
            return this;
        }
    }
}