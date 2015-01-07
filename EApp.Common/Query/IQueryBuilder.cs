using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.QueryPaging;

namespace EApp.Common.Query
{

    public enum Operator 
    { 
        Equal,

        NotEqual,

        GreaterThan,

        GreaterThanEqual,

        LessThan,

        LessThanEqual
    }


    /// <summary>
    /// Specifies how items in a list are sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// The items are not sorted
        /// </summary>
        None = 0,

        /// <summary>
        /// The items are sorted in ascending order.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// The items are sorted in descending order.
        /// </summary>
        Descending = 2,
    }

    public interface IQueryBuilder<TEntity, TIdentityKey> : IQuery<TEntity, TIdentityKey> where TEntity : class, IEntity<TIdentityKey>
    {
        Expression<Func<TEntity, bool>> QueryPredicate { get; }

        IQueryBuilder<TEntity, TIdentityKey> Filter(Expression<Func<TEntity, bool>> predicate, bool isOr = false);

        IQueryBuilder<TEntity, TIdentityKey> Filter(string propertyName, object value, Operator @operator = Operator.Equal);

        IQueryBuilder<TEntity, TIdentityKey> Filter<TPropertyType>(Expression<Func<TEntity, TPropertyType>> propertyExpression,
                                                                   TPropertyType minValue,
                                                                   TPropertyType maxValue) where TPropertyType : struct;

        IQueryBuilder<TEntity, TIdentityKey> And(IQueryBuilder<TEntity, TIdentityKey> queryBuilder);

        IQueryBuilder<TEntity, TIdentityKey> And(Expression<Func<TEntity, bool>> predicate);

        IQueryBuilder<TEntity, TIdentityKey> Or(IQueryBuilder<TEntity, TIdentityKey> queryBuilder);

        IQueryBuilder<TEntity, TIdentityKey> Or(Expression<Func<TEntity, bool>> predicate);

        IQueryBuilder<TEntity, TIdentityKey> AndNot(IQueryBuilder<TEntity, TIdentityKey> queryBuilder);

        IQueryBuilder<TEntity, TIdentityKey> AndNot(Expression<Func<TEntity, bool>> predicate);

        IQueryBuilder<TEntity, TIdentityKey> OrderBy(string propertyName, SortOrder sortOrder = SortOrder.Ascending);

        IQueryBuilder<TEntity, TIdentityKey> OrderBy(Expression<Func<TEntity, dynamic>> predicate, SortOrder sortOrder = SortOrder.Ascending);

    }
}
