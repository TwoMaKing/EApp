using Dappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Dapper
{
    public class DbQuery<T> : IDbQuery<T>
    {
        private IDbConnection connection;

        private IQueryProvider queryProvider;
        
        private Expression expression;

        private IEnumerable<T> queryResult;

        public DbQuery(IDbConnection connection, IQueryProvider queryProvider)
            : this(connection, queryProvider, (Type)null)
        { 
            
        }

        public DbQuery(IDbConnection connection, IQueryProvider queryProvider, Type type)
        {
            if (queryProvider == null)
            {
                throw new ArgumentNullException("Query Provider");
            }

            this.connection = connection;
            this.queryProvider = queryProvider;
            this.expression = type != null ? Expression.Constant(this, type) : Expression.Constant(this);
        }

        public DbQuery(IDbConnection connection, IQueryProvider queryProvider, Expression expression)
        {
            if (queryProvider == null)
            {
                throw new ArgumentNullException("Query Provider");
            }

            this.connection = connection;
            this.queryProvider = queryProvider;
            this.expression = expression ?? Expression.Constant(this, typeof(T));
        }

        #region Private methods

        private IEnumerable<T> GetQueryResult()
        {
            if (queryResult == null)
            {
                queryResult = this.connection.Query<T>(null, null);
            }
            return queryResult;
        }

        #endregion

        public Type ElementType
        {
            get 
            {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get 
            { 
                return this.expression; 
            }
        }

        public IQueryProvider Provider
        {
            get 
            {
                return this.queryProvider;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetQueryResult().GetEnumerator();
        }

        public bool ContainsListCollection
        {
            get 
            {
                return true; 
            }
        }

        public IList GetList()
        {
            return this.GetQueryResult().ToList();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetQueryResult().GetEnumerator();
        }
    }
}
