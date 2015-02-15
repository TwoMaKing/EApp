using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public abstract class SqlStatementFactory : ISqlStatementFactory
    {
        public abstract string CreateInsertSqlStatement(string tableName, string[] columns = null);

        public abstract string CreateUpdateSqlStatement(string tableName, string[] columns, string whereSql);

        public abstract string CreateDeleteSqlStatement(string tableName, string whereSql);

        public abstract string CreateSelectSqlStatement(string tableNames, string whereSql, string orderBy, params string[] columns);

        public virtual string CreateSelectRangeSqlStatement(string tableNames, string whereSql, string orderBy, string groupBy, int topCount, int skipCount, string identityColumn, bool identityColumnIsNumber = true, params string[] columns)
        {
            if (columns == null)
            {
                columns = new string[] { "*" };
            }

            if (topCount == int.MaxValue &&
                skipCount == 0)
            {
                return CreateSelectSqlStatement(tableNames, whereSql, orderBy, columns);
            }
            else if (skipCount == 0)
            {
                return CreateSelectTopSqlStatement(tableNames, whereSql, columns, orderBy, groupBy, topCount);
            }
            else
            {
                if (identityColumnIsNumber &&
                    SqlQueryUtils.OrderByStartsWith(orderBy, identityColumn) &&
                   (string.IsNullOrEmpty(groupBy) ||
                    groupBy.Equals(identityColumn, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return CreateSelectRangeSqlStatementForSortedRows(tableNames, whereSql, columns, orderBy, groupBy,
                                                                      topCount, skipCount, identityColumn, 
                                                                      SqlQueryUtils.OrderByStartsWith(orderBy, identityColumn + " DESC"));
                }
                else
                {
                    return CreateSelectRangeSqlStatementForUnsortedRows(tableNames, whereSql, columns, orderBy, groupBy, topCount, skipCount, identityColumn);
                }
            }
        }

        protected abstract string CreateSelectTopSqlStatement(string tableNames, string whereSql, string[] columns, string orderBy, string groupBy, int topCount);

        protected abstract string CreateSelectRangeSqlStatementForSortedRows(string tableNames, string whereSql, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identityColumn, bool isIdentityColumnDesc);

        protected abstract string CreateSelectRangeSqlStatementForUnsortedRows(string tableNames, string whereSql, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identyColumn);

    }
}
