using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EApp.Common.Lambda;
using EApp.Common.Util;
using EApp.Core.DynamicQuery;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;

namespace EApp.Common.Query
{
    public class QueryBuilder<T> : IQueryBuilder<T> where T : class
    {
        protected ISpecification<T> Specification { get; set; }

        protected OrderByBuilder orderByBuilder = new OrderByBuilder();

        protected Dictionary<Expression<Func<T, dynamic>>, SortOrder> orderByExpressionBuilder = 
            new Dictionary<Expression<Func<T, dynamic>>, SortOrder>();

        protected ExpressionBuilder<T> expressionBuilder = new ExpressionBuilder<T>();

        public QueryBuilder() { }

        public Expression<Func<T, bool>> QueryPredicate
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

        public IQueryBuilder<T> Filter(Expression<Func<T, bool>> predicate, bool isOr = false)
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

        public IQueryBuilder<T> Filter(string propertyName, object value, Operator @operator = Operator.Equal)
        {
            return Filter(LambdaUtil.ParsePredicate<T>(propertyName, value, @operator));
        }

        public IQueryBuilder<T> Filter<TPropertyType>(Expression<Func<T, TPropertyType>> propertyExpression,
                                                      TPropertyType minValue,
                                                      TPropertyType maxValue) where TPropertyType : struct
        {
            Expression leftExpression = expressionBuilder.Create<TPropertyType>(propertyExpression, Operator.GreaterThanEqual, minValue);

            Expression rightExpression = expressionBuilder.Create<TPropertyType>(propertyExpression, Operator.LessThanEqual, maxValue);

            BinaryExpression andExpression =  Expression.Add(leftExpression, rightExpression);

            Expression<Func<T, bool>> rangeFilterExpression = Expression.Lambda<Func<T, bool>>(andExpression);

            this.Filter(rangeFilterExpression);

            return this;
        }

        public IQueryBuilder<T> And(IQueryBuilder<T> queryBuilder)
        {
            return this.And(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<T> And(Expression<Func<T, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<T>(predicate);
            }
            else
            {
                this.Specification = new AndSpecification<T>(this.Specification, new ExpressionSpecification<T>(predicate));
            }

            return this;
        }

        public IQueryBuilder<T> Or(IQueryBuilder<T> queryBuilder)
        {
            return this.Or(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<T> Or(Expression<Func<T, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<T>(predicate);
            }
            else
            {
                this.Specification = new OrSpecification<T>(this.Specification, new ExpressionSpecification<T>(predicate));
            }

            return this;
        }

        public IQueryBuilder<T> AndNot(IQueryBuilder<T> queryBuilder)
        {
            return this.AndNot(queryBuilder.QueryPredicate);
        }

        public IQueryBuilder<T> AndNot(Expression<Func<T, bool>> predicate)
        {
            if (this.Specification == null)
            {
                this.Specification = new ExpressionSpecification<T>(predicate);
            }
            else
            {
                this.Specification = new AndNotSpecification<T>(this.Specification, new ExpressionSpecification<T>(predicate));
            }

            return this;
        }

        public IQueryBuilder<T> OrderBy(string propertyName, SortOrder sortOrder = SortOrder.Ascending)
        {
            this.orderByBuilder.Add(propertyName, sortOrder);

            return this;
        }

        public IQueryBuilder<T> OrderBy(Expression<Func<T, dynamic>> predicate, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (!this.orderByExpressionBuilder.ContainsKey(predicate))
            {
                this.orderByExpressionBuilder.Add(predicate, sortOrder);
            }

            return this;
        }

        public IList<T> ToList(IEnumerable<T> querySource)
        {
            if (querySource == null)
            {
                return null;
            }

            IQueryable<T> queryList = querySource.AsQueryable();

            if (this.QueryPredicate != null)
            {
                queryList = queryList.Where(this.QueryPredicate);
            }

            return this.OrderBy(queryList).ToList();
        }

        public IPagingResult<T> ToPagedList(IEnumerable<T> querySource, int pageNumber, int pageSize)
        {
            return new PagingResult<T>(null, null, pageNumber, pageSize, null);
        }

        private IQueryable<T> OrderBy(IQueryable<T> queryable)
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
                foreach(KeyValuePair<Expression<Func<T, dynamic>>, SortOrder> orderExpressionPair in this.orderByExpressionBuilder)
                {
                    queryable = queryable.SortBy(orderExpressionPair.Key.Compile(), orderExpressionPair.Value).AsQueryable(); 
                }
            }

            return queryable;
        }
    }
}
