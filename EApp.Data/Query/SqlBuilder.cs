using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Query;
using EApp.Data.Query.Criterias;

namespace EApp.Data.Query
{
    public class SqlBuilder : ISqlBuilder
    {
        private string fromTable = string.Empty;

        private List<string> innerJoinTables = new List<string>();

        private List<string> leftOuterJoinTables = new List<string>();

        private List<string> rightOuterJoinTables = new List<string>();

        private string sqlPredicate = string.Empty;

        public ISqlBuilder From(string table)
        {
            if (string.IsNullOrEmpty(table) ||
                string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException("The table cannot be null or empty.");
            }

            this.fromTable = table;

            return this;
        }

        public ISqlBuilder InnerJoin(string joinTable, string fromKey, string joinKey)
        {
            this.innerJoinTables.Add(" inner join " + joinTable + " on " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder LeftOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.leftOuterJoinTables.Add(" left join " + joinTable + " on " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder RightOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.rightOuterJoinTables.Add(" right join " + joinTable + " on " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder And(string criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder And(ISqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Or(string criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Or(ISqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Filter(string column, Operator @operator, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Equals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder NotEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Contains(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder StartsWith(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder EndsWith(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder GreaterThan(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder GreaterThanEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder LessThan(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder LessThanEquals(string column, object value, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder In(string column, IEnumerable<object> values, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder NotIn(string column, IEnumerable<object> values, bool isOr = false)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder OrderBy(string column, SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder GroupBy(string[] columns, string having = "")
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Top(int count)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Count(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Max(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Min(string column)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Select(string[] columns)
        {
            throw new NotImplementedException();
        }

        public ISqlBuilder Where(string wherePredicate, IEnumerable<object> paramValues)
        {
            throw new NotImplementedException();
        }

        public string GetPredicate()
        {
            throw new NotImplementedException();
        }

        public string GetQuerySql()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
