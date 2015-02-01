using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.Query;

namespace EApp.Common.Query
{
    public interface IQueryBuilder<T> : IQuery<T> where T : class
    {
        Expression<Func<T, bool>> QueryPredicate { get; }

        IQueryBuilder<T> Filter(Expression<Func<T, bool>> predicate, bool isOr = false);

        IQueryBuilder<T> Filter(string propertyName, object value, Operator @operator = Operator.Equal);

        IQueryBuilder<T> Filter<TPropertyType>(Expression<Func<T, TPropertyType>> propertyExpression,
                                               TPropertyType minValue,
                                               TPropertyType maxValue) where TPropertyType : struct;

        IQueryBuilder<T> And(IQueryBuilder<T> queryBuilder);

        IQueryBuilder<T> And(Expression<Func<T, bool>> predicate);

        IQueryBuilder<T> Or(IQueryBuilder<T> queryBuilder);

        IQueryBuilder<T> Or(Expression<Func<T, bool>> predicate);

        IQueryBuilder<T> AndNot(IQueryBuilder<T> queryBuilder);

        IQueryBuilder<T> AndNot(Expression<Func<T, bool>> predicate);

        IQueryBuilder<T> OrderBy(string propertyName, SortOrder sortOrder = SortOrder.Ascending);

        IQueryBuilder<T> OrderBy(Expression<Func<T, dynamic>> predicate, SortOrder sortOrder = SortOrder.Ascending);
    }
}
