using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Query;

namespace EApp.Common.Lambda
{
    public class ExpressionBuilder<TEntity>
    {
        public Expression Create<TPropertyType>(Expression<Func<TEntity, TPropertyType>> propertyExpression, 
                                                 Operator @operator,
                                                 TPropertyType value) 
        {
            if (propertyExpression == null ||
               !(propertyExpression.Body is MemberExpression) ||
               ((MemberExpression)propertyExpression.Body).Member.MemberType != System.Reflection.MemberTypes.Property) 
            {
                return null;
            }

            ConstantExpression constantExpression = Expression.Constant(value);

            BinaryExpression filterExpression = null;

            if (@operator == Operator.Equal)
            {
                filterExpression = Expression.Equal(propertyExpression, constantExpression);
            }
            else if (@operator == Operator.NotEqual)
            {
                filterExpression = Expression.NotEqual(propertyExpression, constantExpression);
            }
            else if (@operator == Operator.GreaterThan)
            {
                filterExpression = Expression.GreaterThan(propertyExpression, constantExpression);
            }
            else if (@operator == Operator.GreaterThanEqual)
            {
                filterExpression = Expression.GreaterThanOrEqual(propertyExpression, constantExpression);
            }
            else if (@operator == Operator.LessThan)
            {
                filterExpression = Expression.LessThan(propertyExpression, constantExpression);
            }
            else
            {
                filterExpression = Expression.LessThanOrEqual(propertyExpression, constantExpression);
            }

            return Expression.Lambda(filterExpression);
        }

    }
}
