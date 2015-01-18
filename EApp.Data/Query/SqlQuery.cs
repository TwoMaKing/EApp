using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.Query;
using EApp.Data.Query.Criterias;
using MySql.Data.MySqlClient;

namespace EApp.Data.Query
{
    public class SqlQuery : ISqlQuery
    {
        public SqlQuery()
            : this(new SqlBuilder()) { }

        public SqlQuery(DbProvider dbProvider)
            : this(new SqlBuilder(dbProvider)) { }

        public SqlQuery(ISqlBuilder sqlBuilder)
        {
            this.SqlBuilder = sqlBuilder;
        }

        public ISqlBuilder SqlBuilder
        {
            get;
            private set;
        }

        public ISqlQuery From(string table)
        {
            this.SqlBuilder.From(table);

            return this;
        }

        public ISqlQuery InnerJoin(string joinTable, string fromKey, string joinKey)
        {
            this.SqlBuilder.InnerJoin(joinTable, fromKey, joinKey);

            return this;
        }

        public ISqlQuery LeftOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.SqlBuilder.LeftOuterJoin(joinTable, fromKey, joinKey);

            return this;
        }

        public ISqlQuery RightOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.SqlBuilder.RightOuterJoin(joinTable, fromKey, joinKey);

            return this;
        }

        public ISqlQuery And(string criteria)
        {
            this.SqlBuilder.And(criteria);

            return this;
        }

        public ISqlQuery And(ISqlCriteria criteria)
        {
            this.SqlBuilder.And(criteria);

            return this;
        }

        public ISqlQuery Or(string criteria)
        {
            this.SqlBuilder.Or(criteria);

            return this;
        }

        public ISqlQuery Or(ISqlCriteria criteria)
        {
            this.SqlBuilder.Or(criteria);

            return this;
        }

        public ISqlQuery Filter(string column, Operator @operator, object value, bool isOr = false)
        {
            this.SqlBuilder.Filter(column, @operator, value, isOr);

            return this;
        }

        public ISqlQuery Equals(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.Equals(column, value, isOr);

            return this;
        }

        public ISqlQuery NotEquals(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.NotEquals(column, value, isOr);

            return this;
        }

        public ISqlQuery Contains(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.Contains(column, value, isOr);

            return this;
        }

        public ISqlQuery StartsWith(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.StartsWith(column, value, isOr);

            return this;
        }

        public ISqlQuery EndsWith(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.EndsWith(column, value, isOr);

            return this;
        }

        public ISqlQuery GreaterThan(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.GreaterThan(column, value, isOr);

            return this;
        }

        public ISqlQuery GreaterThanEquals(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.GreaterThanEquals(column, value, isOr);

            return this;
        }

        public ISqlQuery LessThan(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.LessThan(column, value, isOr);

            return this;
        }

        public ISqlQuery LessThanEquals(string column, object value, bool isOr = false)
        {
            this.SqlBuilder.LessThanEquals(column, value, isOr);

            return this;
        }

        public ISqlQuery In(string column, IEnumerable<object> values, bool isOr = false)
        {
            this.SqlBuilder.In(column, values, isOr);

            return this;
        }

        public ISqlQuery NotIn(string column, IEnumerable<object> values, bool isOr = false)
        {
            this.SqlBuilder.NotIn(column, values, isOr);

            return this;
        }

        public ISqlQuery OrderBy(string column, EApp.Core.Query.SortOrder sortOrder)
        {
            this.SqlBuilder.OrderBy(column, sortOrder);

            return this;
        }

        public ISqlQuery GroupBy(string[] columns, string having = "")
        {
            this.SqlBuilder.GroupBy(columns, having);

            return this;
        }

        public ISqlQuery Top(int count)
        {
            this.SqlBuilder.Top(count);

            return this;
        }

        public ISqlQuery Distinct()
        {
            this.SqlBuilder.Distinct();

            return this;
        }

        public ISqlQuery Count(string column)
        {
            this.SqlBuilder.Count(column);

            return this;
        }

        public ISqlQuery Max(string column)
        {
            this.SqlBuilder.Max(column);

            return this;
        }

        public ISqlQuery Min(string column)
        {
            this.SqlBuilder.Min(column);

            return this;
        }

        public ISqlQuery Sum(string column)
        {
            this.SqlBuilder.Sum(column);

            return this;
        }

        public ISqlQuery Select(string[] columns)
        {
            this.SqlBuilder.Select(columns);

            return this;
        }

        public ISqlQuery Where(string wherePredicate, IEnumerable<object> paramValues)
        {
            this.SqlBuilder.Where(wherePredicate, paramValues);

            return this;
        }

        public ISqlQuery Clear()
        {
            this.SqlBuilder.Clear();

            return this;
        }

        public IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction = null)
        {
            string commandSql = this.SqlBuilder.GetQuerySql();
            
            object[] paramValues = this.SqlBuilder.GetParameters().Values.ToArray();

            return null;
        }

        public DataSet ExecuteDataSet(IDbConnection connection, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.SqlBuilder != null)
            {
                this.SqlBuilder.Clear();

                this.SqlBuilder = null;
            }
        }
    }
}
