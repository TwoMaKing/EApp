using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EApp.Common.Lambda;
using EApp.Common.Util;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DynamicQuery;
using EApp.Core.QueryPaging;
using EApp.Core.QuerySepcifications;

namespace EApp.Common.Query
{
    public class QueryBuilder<TEntity, TIdentityKey> : IQueryBuilder<TEntity, TIdentityKey>
        where TEntity : class, IEntity<TIdentityKey>
    {

        protected ISpecification<TEntity> Specification { get; set; }

        protected OrderByBuilder orderByBuilder = new OrderByBuilder();

        protected Dictionary<Expression<Func<TEntity, dynamic>>, SortOrder> orderByExpressionBuilder = 
            new Dictionary<Expression<Func<TEntity, dynamic>>, SortOrder>();

        protected ExpressionBuilder<TEntity> expressionBuilder = new ExpressionBuilder<TEntity>();

        public QueryBuilder() { }

        public Expression<Func<TEntity, bool>> QueryPredicate
        {
            get 
            {
                if (this.Specification == null)
                {
                    return null;
                }

                return this.Specification.GetExpression(); 
            }
        }

        public IQueryBuilder<TEntity, TIdentityKey> Filter(Expression<Func<TEntity, bool>> predicate, bool isOr = false)
        {
            if (predicate == null)
            {
                return null;
            }

            if (isOr)
            {
                this.Or(predicate);
            }
            else
            {
                this.And(predicate);
            }

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> Filter(string propertyName, object value, Operator @operator = Operator.Equal)
        {
            return Filter(LambdaUtil.ParsePredicate<TEntity>(propertyName, value, @operator));
        }

        public IQueryBuilder<TEntity, TIdentityKey> Filter<TPropertyType>(Expression<Func<TEntity, TPropertyType>> propertyExpression,
                                                                          TPropertyType minValue,
                                                                          TPropertyType maxValue) where TPropertyType : struct
        {
            Expression leftExpression = expressionBuilder.Create<TPropertyType>(propertyExpression, Operator.GreaterThanEqual, minValue);

            Expression rightExpression = expressionBuilder.Create<TPropertyType>(propertyExpression, Operator.LessThanEqual, maxValue);

            BinaryExpression andExpression =  Expression.Add(leftExpression, rightExpression);

            Expression<Func<TEntity, bool>> rangeFilterExpression = Expression.Lambda<Func<TEntity, bool>>(andExpression);

            this.Filter(rangeFilterExpression);

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> And(IQueryBuilder<TEntity, TIdentityKey> queryBuilder)
        {
            return this.And(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<TEntity, TIdentityKey> And(Expression<Func<TEntity, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<TEntity>(predicate);
            }
            else
            {
                this.Specification = new AndSpecification<TEntity>(this.Specification,
                                                                   new ExpressionSpecification<TEntity>(predicate));
            }

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> Or(IQueryBuilder<TEntity, TIdentityKey> queryBuilder)
        {
            return this.Or(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<TEntity, TIdentityKey> Or(Expression<Func<TEntity, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<TEntity>(predicate);
            }
            else
            {
                this.Specification = new OrSpecification<TEntity>(this.Specification,
                                                                  new ExpressionSpecification<TEntity>(predicate));
            }

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> AndNot(IQueryBuilder<TEntity, TIdentityKey> queryBuilder)
        {
            return this.AndNot(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<TEntity, TIdentityKey> AndNot(Expression<Func<TEntity, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<TEntity>(predicate);
            }
            else
            {
                this.Specification = new AndNotSpecification<TEntity>(this.Specification,
                                                                      new ExpressionSpecification<TEntity>(predicate));
            }

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> OrderBy(string propertyName, SortOrder sortOrder = SortOrder.Ascending)
        {
            this.orderByBuilder.Add(propertyName, sortOrder);

            return this;
        }

        public IQueryBuilder<TEntity, TIdentityKey> OrderBy(Expression<Func<TEntity, dynamic>> predicate, 
                                                            SortOrder sortOrder = SortOrder.Ascending)
        {
            if (!this.orderByExpressionBuilder.ContainsKey(predicate))
            {
                this.orderByExpressionBuilder.Add(predicate, sortOrder);
            }

            //this.orderByBuilder.Add(LambdaUtil.GetMemberName(predicate), sortOrder);

            return this;
        }

        public IList<TEntity> ToList(IEnumerable<TEntity> querySource)
        {
            if (querySource == null)
            {
                return null;
            }

            IQueryable<TEntity> queryList = querySource.AsQueryable();

            if (this.QueryPredicate != null)
            {
                queryList = queryList.Where(this.QueryPredicate);
            }

            return this.OrderBy(queryList).ToList();
        }

        public IPagingResult<TEntity> ToPagedList(IEnumerable<TEntity> querySource, int? pageNumber, int? pageSize)
        {
            return new PagingResult<TEntity>(null, null, pageNumber, pageSize, null);
        }

        private IQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable)
        {
            if (this.orderByBuilder != null &&
                this.orderByBuilder.OrderByItems != null &&
                this.orderByBuilder.OrderByItems.Count > 0)
            {
                queryable = queryable.OrderBy(this.orderByBuilder.ToString());
            }

            if (this.orderByExpressionBuilder != null &&
                this.orderByExpressionBuilder.Count > 0)
            { 
                foreach(KeyValuePair<Expression<Func<TEntity, dynamic>>, SortOrder> orderExpressionPair in this.orderByExpressionBuilder)
                {
                    queryable = queryable.SortBy(orderExpressionPair.Key.Compile(), orderExpressionPair.Value).AsQueryable(); 
                }
            }

            return queryable;
        }
    }
}
