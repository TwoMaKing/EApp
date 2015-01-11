using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Data.Query
{
    public class SqlQuery : ISqlQuery
    {
        public ISqlBuilder SqlBuilder
        {
            get { throw new NotImplementedException(); }
        }

        public ISqlQuery From(string table)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery InnerJoin(string joinTable, string fromKey, string joinKey)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery LeftOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery RightOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery And(string criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery And(Criterias.ISqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Or(string criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Or(Criterias.ISqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Filter(string column, Core.Query.Operator @operator, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Equals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery NotEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Contains(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery StartsWith(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery EndsWith(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery GreaterThan(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery GreaterThanEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery LessThan(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery LessThanEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery In(string column, IEnumerable<object> values, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery NotIn(string column, IEnumerable<object> values, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery OrderBy(string column, Core.Query.SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery GroupBy(string[] columns, string having = "")
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Top(int count)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Count(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Max(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Min(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Select(string[] columns)
        {
            throw new NotImplementedException();
        }

        public ISqlQuery Where(string wherePredicate, IEnumerable<object> paramValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(IDbConnection connection, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

    }
}
