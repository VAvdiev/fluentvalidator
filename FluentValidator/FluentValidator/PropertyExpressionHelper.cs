using System;
using System.Linq.Expressions;

namespace FluentValidator
{
    public class PropertyExpressionHelper
    {
        public static string GetPropertyName<TConatainer,TProperty>(Expression<Func<TConatainer,TProperty>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static Func<TContainer, TProperty> InitializeGetter<TContainer, TProperty>(Expression<Func<TContainer, TProperty>> getterExpression)
        {
            return getterExpression.Compile();
        }
    }
}