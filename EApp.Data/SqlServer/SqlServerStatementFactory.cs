using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EApp.Data.SqlServer
{
    public class SqlServerStatementFactory : SqlStatementFactory, ISqlStatementFactory
    {
        private char parameterLeftToken;

        private char parameterRightToken; 
            
        private char parameterPrefix;

        private char wildCharToken;

        private char wildSingleCharToken;

        public SqlServerStatementFactory(DbProvider dbProvider) : base(dbProvider) 
        {
            this.parameterPrefix = dbProvider.ParameterPrefix;
            this.parameterLeftToken = dbProvider.ParameterLeftToken;
            this.parameterRightToken = dbProvider.ParameterRightToken;
            this.wildCharToken = dbProvider.WildCharToken;
            this.wildSingleCharToken = dbProvider.WildSingleCharToken;
        }

        public override string CreateInsertStatement(string tableName, string[] includedColumns)
        {
            string insertSql = @"INSERT INTO [{0}] {1} VALUES ({2})";

            StringBuilder columnNameBuilder = new StringBuilder();
            StringBuilder columnParamNameBuilder = new StringBuilder();

            if (includedColumns != null &&
                includedColumns.Length > 0)
            {
                string includedColumn;

                string dbFieldName;

                string dbFieldParamName;

                for (int columnIndex = 0; columnIndex < includedColumns.Length; columnIndex++)
                {
                    includedColumn = includedColumns[columnIndex].Trim(parameterLeftToken, parameterRightToken, parameterPrefix);

                    dbFieldName = string.Format("{0}{1}{2},", parameterLeftToken, includedColumn, parameterRightToken);

                    if (columnIndex == 0)
                    {
                        dbFieldName = "(" + dbFieldName;
                    }

                    if (columnIndex == includedColumns.Length - 1)
                    {
                        dbFieldName = dbFieldName + ")";
                    }

                    dbFieldParamName = string.Format("{0}{1},", parameterPrefix, includedColumn);

                    columnNameBuilder.Append(dbFieldName);

                    columnParamNameBuilder.Append(dbFieldParamName);
                }
            }
            else
            {
                columnParamNameBuilder.Append("{0}");
            }

            return string.Format(insertSql, tableName.Trim(parameterLeftToken, parameterRightToken),
                columnNameBuilder.ToString().TrimEnd(',', ' '),
                columnParamNameBuilder.ToString().TrimEnd(',', ' '));
        }

        public override string CreateUpdateStatement(string tableName, string where, string[] includedColumns)
        {
            if (includedColumns == null ||
                includedColumns.Length.Equals(0))
            {
                throw new ArgumentNullException("Columns to be updated cannot be null.");
            }

            string updateSql = @"UPDATE [{0}] SET {1} {2}";

            string includedColumn;

            string fieldUpdateStatement;

            StringBuilder fieldUpdateStatementBuilder = new StringBuilder();

            for (int columnIndex = 0; columnIndex < includedColumns.Length; columnIndex++)
            {
                includedColumn = includedColumns[columnIndex].Trim(parameterLeftToken, parameterRightToken, parameterPrefix);

                fieldUpdateStatement = string.Format("{0}{1}{2} = {3}{1},",
                    parameterLeftToken, includedColumn, parameterRightToken, parameterPrefix);

                fieldUpdateStatementBuilder.Append(fieldUpdateStatement);
            }
            
            return string.Format(updateSql, 
                                 tableName.Trim(parameterLeftToken, parameterRightToken),
                                 fieldUpdateStatementBuilder.ToString().TrimEnd(',', ' '),
                                 string.IsNullOrEmpty(where.Trim()) ? string.Empty : "WHERE " + where);
        }

        public override string CreateDeleteStatement(string tableName, string where)
        {
            string deleteSql = @"DELETE FROM [{0}] {1}";

            return string.Format(deleteSql, 
                                 tableName.Trim(parameterLeftToken, parameterRightToken),
                                 string.IsNullOrEmpty(where.Trim()) ? string.Empty : "WHERE " + where);
        }

        public override string CreateSelectStatement(string tableName, string where, string orderBy, params string[] includedColumns)
        {
            string querySelectSql = "SELECT {0} FROM {1} {2} {3}";

            StringBuilder selectedFieldNameBuilder = new StringBuilder();

            if (includedColumns != null)
            {
                string includedColumn;

                for (int columnIndex = 0; columnIndex < includedColumns.Length; columnIndex++)
                {
                    includedColumn = includedColumns[columnIndex].Trim(parameterLeftToken, parameterRightToken, parameterPrefix);
                    selectedFieldNameBuilder.Append(string.Format("{0}, ", includedColumn));
                }
            }
            else
            {
                selectedFieldNameBuilder.Append("*");
            }

            return string.Format(querySelectSql,
                                 selectedFieldNameBuilder.ToString().TrimEnd(',', ' '),
                                 tableName.Trim(parameterLeftToken, parameterRightToken),
                                 string.IsNullOrEmpty(where.Trim()) ? string.Empty : "WHERE " + where,
                                 string.IsNullOrEmpty(orderBy.Trim()) ? string.Empty : "ORDER BY " + orderBy);
        }

        protected override string CreateSelectTopStatement(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount)
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT TOP ");
            sqlBuilder.Append(topCount);
            sqlBuilder.Append(' ');
            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(sqlBuilder, columns[i], parameterLeftToken, parameterRightToken);

                if (i < columns.Length - 1)
                {
                    sqlBuilder.Append(',');
                }
            }

            sqlBuilder.Append(" FROM ");
            sqlBuilder.Append(tableName);
            sqlBuilder.Append(' ');
            sqlBuilder.Append(where);
            sqlBuilder.Append(" ORDER BY " + orderBy);
            sqlBuilder.Append(" GROUP BY " + groupBy);

            return sqlBuilder.ToString();
        }

        protected override string CreateSelectRangeStatementForSortedRows(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identityColumn, bool isIdentityColumnDesc)
        {
            //SELECT TOP 10 *
            //FROM TestTable
            //WHERE (ID >
            //          (SELECT MAX(id)/MIN(id)
            //         FROM (SELECT TOP 20 id
            //                 FROM TestTable
            //                 ORDER BY id) AS T))
            //ORDER BY ID'

            StringBuilder outerSqlBuilder = new StringBuilder("SELECT ");

            if (topCount < int.MaxValue)
            {
                outerSqlBuilder.Append("TOP ");
                outerSqlBuilder.Append(topCount);
                outerSqlBuilder.Append(' ');
            }

            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(outerSqlBuilder, columns[i], parameterLeftToken, parameterRightToken);

                if (i < columns.Length - 1)
                {
                    outerSqlBuilder.Append(',');
                }
            }

            outerSqlBuilder.Append(" FROM ");

            outerSqlBuilder.Append(tableName);

            outerSqlBuilder.Append(" WHERE ");

            StringBuilder innerWhereClipBuilder = new StringBuilder();
            innerWhereClipBuilder.Append(tableName);

            if (!string.IsNullOrEmpty(where) &&
                !string.IsNullOrWhiteSpace(where))
            {
                innerWhereClipBuilder.Append(" WHERE " + where);
            }

            StringBuilder orderByGroupByBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(orderBy) &&
                !string.IsNullOrWhiteSpace(orderBy))
            {
                orderByGroupByBuilder.Append(" ORDER BY " + orderBy);
            }

            if (!string.IsNullOrEmpty(groupBy) &&
                !string.IsNullOrWhiteSpace(groupBy))
            {
                orderByGroupByBuilder.Append(" GROUP BY " + groupBy);
            }

            innerWhereClipBuilder.Append(orderByGroupByBuilder.ToString());

            #region Construct & extend CloneWhere

            StringBuilder innerSqlBuilder = new StringBuilder();
            innerSqlBuilder.Append(identityColumn);
            innerSqlBuilder.Append(isIdentityColumnDesc ? '<' : '>');
            innerSqlBuilder.Append('(');
            innerSqlBuilder.Append("SELECT ");
            innerSqlBuilder.Append(isIdentityColumnDesc ? "MIN(" : "MAX(");
            innerSqlBuilder.Append("[__T].");
            string[] splittedIdentyColumn = identityColumn.Split('.');
            innerSqlBuilder.Append(splittedIdentyColumn[splittedIdentyColumn.Length - 1]);
            innerSqlBuilder.Append(") FROM (SELECT TOP ");
            innerSqlBuilder.Append(skipCount);
            innerSqlBuilder.Append(' ');
            innerSqlBuilder.Append(identityColumn);
            innerSqlBuilder.Append(" AS ");
            innerSqlBuilder.Append(splittedIdentyColumn[splittedIdentyColumn.Length - 1]);
            innerSqlBuilder.Append(" FROM ");
            innerSqlBuilder.Append(innerWhereClipBuilder.ToString());
            innerSqlBuilder.Append(") [__T])");

            string outerWhereSql = string.Empty;

            if (where.Length == 0)
            {
                outerWhereSql = innerSqlBuilder.ToString();
            }
            else
            {
                outerWhereSql = "(" + where + ") AND " + innerSqlBuilder.ToString();
            }

            #endregion

            outerSqlBuilder.Append(outerWhereSql);

            outerSqlBuilder.Append(orderByGroupByBuilder.ToString());

            return SqlQueryUtils.ReplaceDatabaseTokens(outerSqlBuilder.ToString(), this.parameterLeftToken, this.parameterRightToken, this.parameterPrefix, this.wildCharToken, this.wildSingleCharToken);
        }

        protected override string CreateSelectRangeStatementForUnsortedRows(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identyColumn)
        {
            //SELECT TOP 10 *
            //FROM TestTable
            //WHERE (ID NOT IN
            //          (SELECT TOP 20 id
            //         FROM TestTable
            //         ORDER BY id))
            //ORDER BY ID

            StringBuilder outerSqlBuilder = new StringBuilder("SELECT ");

            if (topCount < int.MaxValue)
            {
                outerSqlBuilder.Append("TOP ");
                outerSqlBuilder.Append(topCount);
                outerSqlBuilder.Append(' ');
            }

            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(outerSqlBuilder, columns[i], parameterLeftToken, parameterRightToken);

                if (i < columns.Length - 1)
                {
                    outerSqlBuilder.Append(',');
                }
            }

            outerSqlBuilder.Append(" FROM ");

            outerSqlBuilder.Append(tableName);

            outerSqlBuilder.Append(" WHERE ");

            StringBuilder innerWhereClipBuilder = new StringBuilder();
            innerWhereClipBuilder.Append(tableName);

            if (!string.IsNullOrEmpty(where) &&
                !string.IsNullOrWhiteSpace(where))
            {
                innerWhereClipBuilder.Append(" WHERE " + where);
            }

            StringBuilder orderByGroupByBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(orderBy) &&
                !string.IsNullOrWhiteSpace(orderBy))
            {
                orderByGroupByBuilder.Append(" ORDER BY " + orderBy);
            }

            if (!string.IsNullOrEmpty(groupBy) &&
                !string.IsNullOrWhiteSpace(groupBy))
            {
                orderByGroupByBuilder.Append(" GROUP BY " + groupBy);
            }

            innerWhereClipBuilder.Append(orderByGroupByBuilder.ToString());

            #region Construct & extend CloneWhere

            StringBuilder innerSqlBuilder = new StringBuilder();

            innerSqlBuilder.Append(identyColumn);
            innerSqlBuilder.Append(" NOT IN (SELECT TOP ");
            innerSqlBuilder.Append(skipCount);
            innerSqlBuilder.Append(' ');
            innerSqlBuilder.Append(identyColumn);
            innerSqlBuilder.Append(" FROM ");
            innerSqlBuilder.Append(innerWhereClipBuilder.ToString());
            innerSqlBuilder.Append(")");

            string outerWhereSql = string.Empty;

            if (where.Length == 0)
            {
                outerWhereSql = innerSqlBuilder.ToString();
            }
            else
            {
                outerWhereSql = "(" + where + ") AND " + innerSqlBuilder.ToString();
            }

            #endregion

            outerSqlBuilder.Append(outerWhereSql);

            outerSqlBuilder.Append(orderByGroupByBuilder.ToString());

            return SqlQueryUtils.ReplaceDatabaseTokens(outerSqlBuilder.ToString(), this.parameterLeftToken, this.parameterRightToken, this.parameterPrefix, this.wildCharToken, this.wildSingleCharToken);
       
        }

    }
}
