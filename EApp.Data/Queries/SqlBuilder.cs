using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Exceptions;
using EApp.Core.Query;
using EApp.Data.Queries.Criterias;

namespace EApp.Data.Queries
{
    public class SqlBuilder : ISqlBuilder
    {
        private DbProvider dbProvider;

        private string fromTable = string.Empty;

        private List<string> innerJoinTables = new List<string>();

        private List<string> leftOuterJoinTables = new List<string>();

        private List<string> rightOuterJoinTables = new List<string>();

        private StringBuilder querySqlBuilder = new StringBuilder();

        private List<string> selectColumns = new List<string>();

        private string selectTopSql = string.Empty;

        private string selectDistinctSql = string.Empty;

        private StringBuilder selectFunctionBuilder = new StringBuilder();

        private List<string> orderByColumns = new List<string>();

        private string groupBySql = string.Empty;

        private string identityColumn = string.Empty;

        private int pageNumber = 0;

        private int pageSize = 0;

        private IDictionary<string, object> parameterColumnValues = new Dictionary<string, object>();

        private IList<object> parameterValues = new List<object>();

        public SqlBuilder() : this(DbProviderFactory.Default) { }

        public SqlBuilder(DbProvider dbProvider) 
        {
            this.dbProvider = dbProvider;
        }

        protected ISqlCriteria SqlCriteria
        {
            get;
            set;
        }

        public DbProvider DbProvider
        {
            get 
            {
                return this.dbProvider;
            }
        }

        public ISqlBuilder From(string table)
        {
            if (string.IsNullOrEmpty(table) ||
                string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException("The table cannot be null or empty.");
            }

            if (!string.IsNullOrEmpty(this.fromTable) &&
                !string.IsNullOrWhiteSpace(this.fromTable))
            {
                throw new InfrastructureException("From has already been specified.");
            }

            this.fromTable = table;

            return this;
        }

        public ISqlBuilder InnerJoin(string joinTable, string fromKey, string joinKey)
        {
            this.innerJoinTables.Add(" INNER JOIN " + joinTable + " ON " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder LeftOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.leftOuterJoinTables.Add(" LEFT OUTER JOIN " + joinTable + " ON " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder RightOuterJoin(string joinTable, string fromKey, string joinKey)
        {
            this.rightOuterJoinTables.Add(" RIGHT OUTER JOIN " + joinTable + " ON " + fromKey + " = " + joinKey);

            return this;
        }

        public ISqlBuilder And(string criteria)
        {
            return this.And(new TextSqlCriteria(criteria));
        }

        public ISqlBuilder And(ISqlCriteria criteria)
        {
            if (this.SqlCriteria == null)
            {
                this.SqlCriteria = new TextSqlCriteria(string.Empty);
            }

            this.SqlCriteria = new AndSqlCriteria(this.SqlCriteria, criteria);

            return this;
        }

        public ISqlBuilder Or(string criteria)
        {
            return this.Or(new TextSqlCriteria(criteria));
        }

        public ISqlBuilder Or(ISqlCriteria criteria)
        {
            if (this.SqlCriteria == null)
            {
                this.SqlCriteria = new TextSqlCriteria(string.Empty);
            }

            this.SqlCriteria = new OrSqlCriteria(this.SqlCriteria, criteria);

            return this;
        }

        public ISqlBuilder Filter(string column, Operator @operator, object value, bool isOr = false)
        {
            OperatorSqlCriteria sqlCriteria = OperatorSqlCriteria.Create(this.DbProvider, column, @operator);

            if (isOr)
            {
                this.Or(sqlCriteria);
            }
            else
            {
                this.And(sqlCriteria);
            }

            this.parameterColumnValues.Add(sqlCriteria.ParameterColumn, value);

            this.parameterValues.Add(value);

            return this;
        }

        public ISqlBuilder Equals(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.Equal, value, isOr);
        }

        public ISqlBuilder NotEquals(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.NotEqual, value, isOr);
        }

        public ISqlBuilder Contains(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.Contains, "%" + value + "%", isOr);
        }

        public ISqlBuilder StartsWith(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.StartsWith, "%" + value, isOr);
        }

        public ISqlBuilder EndsWith(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.EndsWith, value + "%", isOr);
        }

        public ISqlBuilder GreaterThan(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.GreaterThan, value, isOr);
        }

        public ISqlBuilder GreaterThanEquals(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.GreaterThanEqual, value, isOr);
        }

        public ISqlBuilder LessThan(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.LessThan, value, isOr);
        }

        public ISqlBuilder LessThanEquals(string column, object value, bool isOr = false)
        {
            return this.Filter(column, Operator.LessThanEqual, value, isOr);
        }

        public ISqlBuilder In(string column, IEnumerable<object> values, bool isOr = false)
        {
            if (values == null ||
                values.Count().Equals(0))
            {
                throw new ArgumentNullException("Values for In cannot be null or empty.");
            }

            List<object> valueList = values.ToList();
            StringBuilder inValueParamBuilder = new StringBuilder();

            for (int valueIndex = 0; valueIndex < valueList.Count; valueIndex++) 
            {
                string valueItem = valueList[valueIndex].ToString();

                inValueParamBuilder.Append(valueItem + (valueIndex < valueList.Count - 1 ? "," : string.Empty));
            }

            return this.Filter(column, Operator.In, inValueParamBuilder.ToString(), isOr);
        }

        public ISqlBuilder NotIn(string column, IEnumerable<object> values, bool isOr = false)
        {
            if (values == null ||
                values.Count().Equals(0))
            {
                throw new ArgumentNullException("Values for Not In cannot be null or empty.");
            }

            List<object> valueList = values.ToList();
            StringBuilder notInValueParamBuilder = new StringBuilder();

            for (int valueIndex = 0; valueIndex < valueList.Count; valueIndex++)
            {
                string valueItem = valueList[valueIndex].ToString();

                notInValueParamBuilder.Append(valueItem + (valueIndex < valueList.Count - 1 ? "," : string.Empty));
            }

            return this.Filter(column, Operator.NotIn, notInValueParamBuilder.ToString(), isOr);
        }

        public ISqlBuilder OrderBy(string column, SortOrder sortOrder)
        {
            string orderByItem = column + (sortOrder == SortOrder.Ascending ? " ASC " : " DESC ");

            this.orderByColumns.Add(orderByItem);

            return this;
        }

        public ISqlBuilder GroupBy(string[] columns, string having = "")
        {
            if (!string.IsNullOrEmpty(this.groupBySql) &&
                !string.IsNullOrWhiteSpace(this.groupBySql))
            {
                throw new InfrastructureException("Group By has already been specified.");
            }

            if (columns == null ||
                columns.Length.Equals(0))
            {
                return this;
            }

            this.groupBySql = string.Join(",", columns) + 
                             (string.IsNullOrEmpty(having) ? 
                              string.Empty :
                              " HAVING " + having);

            return this;
        }

        public ISqlBuilder Top(int count)
        {
            if (!string.IsNullOrEmpty(this.selectTopSql) &&
                !string.IsNullOrWhiteSpace(this.selectTopSql))
            {
                throw new InfrastructureException("Top has already been specified.");
            }

            this.selectTopSql = " TOP " + count.ToString();

            return this;
        }

        public ISqlBuilder Distinct()
        {
            if (!string.IsNullOrEmpty(this.selectDistinctSql) &&
                !string.IsNullOrWhiteSpace(this.selectDistinctSql))
            {
                throw new InfrastructureException("Distinct has already been specified.");
            }

            this.selectDistinctSql = " DISTINCT ";

            return this;
        }

        public ISqlBuilder Count(string column)
        {
            this.selectFunctionBuilder.Append(" COUNT (" + column + "), ");

            return this;
        }

        public ISqlBuilder Max(string column)
        {
            this.selectFunctionBuilder.Append(" MAX (" + column + "), ");

            return this;
        }

        public ISqlBuilder Min(string column)
        {
            this.selectFunctionBuilder.Append(" MIN (" + column + "), ");

            return this;
        }

        public ISqlBuilder Sum(string column)
        {
            this.selectFunctionBuilder.Append(" SUM (" + column + "), ");

            return this;
        }

        public ISqlBuilder Select(params string[] columns)
        {
            if (columns == null)
            {
                if (!this.selectColumns.Contains("*"))
                {
                    this.selectColumns.Add("*");
                }
            }
            else
            {
                this.selectColumns.AddRange(columns);
            }

            return this;
        }

        public ISqlBuilder Where(string wherePredicate, IEnumerable<object> paramValues)
        {
            return this;
        }

        public ISqlBuilder Page(string identityColumn, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            }

            this.identityColumn = identityColumn;

            this.pageNumber = pageNumber;

            this.pageSize = pageSize;

            return this;
        }

        public ISqlBuilder Clear()
        {
            this.querySqlBuilder.Clear();
            this.selectFunctionBuilder.Clear();

            this.innerJoinTables.Clear();
            this.leftOuterJoinTables.Clear();
            this.rightOuterJoinTables.Clear();
            this.selectColumns.Clear();
            this.orderByColumns.Clear();
            this.parameterValues.Clear();
            this.parameterColumnValues.Clear();

            this.fromTable = string.Empty;
            this.selectTopSql = string.Empty;
            this.selectDistinctSql = string.Empty;
            this.groupBySql = string.Empty;

            ParameterColumnCache.Instance.Reset();

            return this;
        }

        public string GetTables()
        {
            StringBuilder tableBuilder = new StringBuilder();

            tableBuilder.Append(this.fromTable);

            if (this.innerJoinTables != null &&
                this.innerJoinTables.Count > 0)
            {
                this.innerJoinTables.ForEach((joinSql) => tableBuilder.Append(joinSql));
            }

            if (this.leftOuterJoinTables != null &&
                this.leftOuterJoinTables.Count > 0)
            {
                this.leftOuterJoinTables.ForEach((joinSql) => tableBuilder.Append(joinSql));
            }

            if (this.rightOuterJoinTables != null &&
                this.rightOuterJoinTables.Count > 0)
            {
                this.rightOuterJoinTables.ForEach((joinSql) => tableBuilder.Append(joinSql));
            }

            return tableBuilder.ToString();
        }

        public string[] GetColumns()
        {
            return this.selectColumns.ToArray();
        }

        public string GetOrderBy()
        {
            return string.Join(",", this.orderByColumns.ToArray());
        }

        public string GetGroupBy()
        {
            return this.groupBySql;
        }

        public string GetPredicate()
        {
            if (this.SqlCriteria == null)
            {
                return string.Empty;
            }

            return this.SqlCriteria.GetSqlCriteria();
        }

        public string GetQuerySql()
        {
            this.querySqlBuilder = new StringBuilder();

            this.querySqlBuilder.Append("SELECT ")
                                .Append(this.selectDistinctSql)
                                .Append(this.selectTopSql)
                                .Append(this.selectFunctionBuilder.ToString().TrimEnd(new char[] { ',', ' ' }))
                                .Append(this.selectColumns.Count.Equals(0) ? "*" : string.Join(",", this.selectColumns))
                                .Append(" FROM ")
                                .Append(this.GetTables());

            string sqlPredicate = this.GetPredicate();

            if (!string.IsNullOrEmpty(sqlPredicate))
            {
                this.querySqlBuilder.Append(" WHERE ");
                this.querySqlBuilder.Append(sqlPredicate);
            }

            if (!string.IsNullOrEmpty(this.groupBySql) &&
                !string.IsNullOrWhiteSpace(this.groupBySql))
            {
                this.querySqlBuilder.Append(" GROUP BY ");
                this.querySqlBuilder.Append(this.groupBySql);
            }
       
            if (this.orderByColumns.Count > 0)
            {
                this.querySqlBuilder.Append(" ORDER BY ");
                this.querySqlBuilder.Append(this.GetOrderBy()); 
            }

            return this.querySqlBuilder.ToString();
        }

        public IDictionary<string, object> GetParameters()
        {
            return this.parameterColumnValues;
        }

    }
}
