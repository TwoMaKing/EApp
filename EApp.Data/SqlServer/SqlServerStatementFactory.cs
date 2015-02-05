using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EApp.Data.SqlServer
{
    public class SqlServerStatementFactory : SqlStatementFactory, ISqlStatementFactory
    {
        private const char Parameter_Prefix = '@';

        private const char Parameter_Left_Token = '[';

        private const char Parameter_Right_Token = ']';

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
                    includedColumn = includedColumns[columnIndex].Trim(Parameter_Left_Token, Parameter_Right_Token, Parameter_Prefix);

                    dbFieldName = string.Format("{0}{1}{2},", Parameter_Left_Token, includedColumn, Parameter_Right_Token);

                    if (columnIndex == 0)
                    {
                        dbFieldName = "(" + dbFieldName;
                    }

                    if (columnIndex == includedColumns.Length - 1)
                    {
                        dbFieldName = dbFieldName + ")";
                    }

                    dbFieldParamName = string.Format("{0}{1},", Parameter_Prefix, includedColumn);

                    columnNameBuilder.Append(dbFieldName);

                    columnParamNameBuilder.Append(dbFieldParamName);
                }
            }
            else
            {
                columnParamNameBuilder.Append("{0}");
            }

            return string.Format(insertSql, tableName.Trim(Parameter_Left_Token, Parameter_Right_Token),
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
                includedColumn = includedColumns[columnIndex].Trim(Parameter_Left_Token, Parameter_Right_Token, Parameter_Prefix);

                fieldUpdateStatement = string.Format("{0}{1}{2} = {3}{1},",
                    Parameter_Left_Token, includedColumn, Parameter_Right_Token, Parameter_Prefix);

                fieldUpdateStatementBuilder.Append(fieldUpdateStatement);
            }
            
            return string.Format(updateSql, 
                                 tableName.Trim(Parameter_Left_Token, Parameter_Right_Token),
                                 fieldUpdateStatementBuilder.ToString().TrimEnd(',', ' '),
                                 string.IsNullOrEmpty(where.Trim()) ? string.Empty : "WHERE " + where);
        }

        public override string CreateDeleteStatement(string tableName, string where)
        {
            string deleteSql = @"DELETE FROM [{0}] {1}";

            return string.Format(deleteSql, 
                                 tableName.Trim(Parameter_Left_Token, Parameter_Right_Token),
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
                    includedColumn = includedColumns[columnIndex].Trim(Parameter_Left_Token, Parameter_Right_Token, Parameter_Prefix);
                    selectedFieldNameBuilder.Append(string.Format("{0}, ", includedColumn));
                }
            }
            else
            {
                selectedFieldNameBuilder.Append("*");
            }

            return string.Format(querySelectSql,
                                 selectedFieldNameBuilder.ToString().TrimEnd(',', ' '),
                                 tableName.Trim(Parameter_Left_Token, Parameter_Right_Token),
                                 string.IsNullOrEmpty(where.Trim()) ? string.Empty : "WHERE " + where,
                                 string.IsNullOrEmpty(orderBy.Trim()) ? string.Empty : "ORDER BY " + orderBy);
        }

        public override string CreateSelectRangeStatement(string from, string where, string orderBy, int topCount, int skipCount, string identityColumn, [OptionalAttribute][DefaultParameterValueAttribute(true)]bool identityColumnIsNumber, [OptionalAttribute][DefaultParameterValueAttribute(null)]string groupBy, params string[] includedColumns)
        {
            return base.CreateSelectRangeStatement(from, where, orderBy, topCount, skipCount, identityColumn, identityColumnIsNumber, groupBy, includedColumns);
        }

        protected override string CreateSelectTopStatement(string from, string where, string[] columns, string orderBy, string groupBy, int topCount)
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT TOP ");
            sqlBuilder.Append(topCount);
            sqlBuilder.Append(' ');
            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(sqlBuilder, columns[i], Parameter_Left_Token, Parameter_Right_Token);

                if (i < columns.Length - 1)
                {
                    sqlBuilder.Append(',');
                }
            }

            sqlBuilder.Append(" FROM ");
            sqlBuilder.Append(from);
            sqlBuilder.Append(' ');
            sqlBuilder.Append(where);
            sqlBuilder.Append(" ORDER BY " + orderBy);
            sqlBuilder.Append(" GROUP BY " + groupBy);

            return sqlBuilder.ToString();
        }

        protected override string CreateSelectRangeStatementForSortedRows(string from, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identityColumn, bool isIdentityColumnDesc)
        {
            //SELECT TOP 10 *
            //FROM TestTable
            //WHERE (ID >
            //          (SELECT MAX(id)/MIN(id)
            //         FROM (SELECT TOP 20 id
            //                 FROM TestTable
            //                 ORDER BY id) AS T))
            //ORDER BY ID'

            StringBuilder columnSqlBuilder = new StringBuilder();

            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(columnSqlBuilder, columns[i], Parameter_Left_Token, Parameter_Right_Token);

                if (i < columns.Length - 1)
                {
                    columnSqlBuilder.Append(',');
                }
            }

            string columnSql = columnSqlBuilder.ToString();

            StringBuilder sourceTableSqlBuilder = new StringBuilder("(SELECT ");
            sourceTableSqlBuilder.Append(columnSql);
            sourceTableSqlBuilder.Append(" FROM ");
            sourceTableSqlBuilder.Append(from);
            sourceTableSqlBuilder.Append(' ');
            sourceTableSqlBuilder.Append(where);
            sourceTableSqlBuilder.Append(" ORDER BY " + orderBy);
            sourceTableSqlBuilder.Append(" GROUP BY " + groupBy);
            sourceTableSqlBuilder.Append(") ");

            StringBuilder outerSqlBuilder = new StringBuilder("SELECT ");
            if (topCount < int.MaxValue)
            {
                outerSqlBuilder.Append("TOP ");
                outerSqlBuilder.Append(topCount);
                outerSqlBuilder.Append(' ');
            }
            outerSqlBuilder.Append(columnSql);
            outerSqlBuilder.Append(" FROM ");

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
            innerSqlBuilder.Append(sourceTableSqlBuilder.ToString());
            innerSqlBuilder.Append(") [__T])");

            string outerWhereSql = string.Empty;

            if (sourceTableSqlBuilder.Length == 0)
            {
                outerWhereSql = innerSqlBuilder.ToString();
            }
            else
            {
                outerWhereSql = "(" + sourceTableSqlBuilder.ToString() + ") AND " + innerSqlBuilder.ToString();
            }

            #endregion

            outerSqlBuilder.Append(outerWhereSql);

            return outerSqlBuilder.ToString();
        }

        protected override string CreateSelectRangeStatementForUnsortedRows(string from, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identyColumn)
        {
            //SELECT TOP 10 *
            //FROM TestTable
            //WHERE (ID NOT IN
            //          (SELECT TOP 20 id
            //         FROM TestTable
            //         ORDER BY id))
            //ORDER BY ID

            StringBuilder sb = new StringBuilder("SELECT ");
            if (topCount < int.MaxValue)
            {
                sb.Append("TOP ");
                sb.Append(topCount);
                sb.Append(' ');
            }
            for (int i = 0; i < columns.Length; ++i)
            {
                SqlQueryUtils.AppendColumnName(sb, columns[i], Parameter_Left_Token, Parameter_Right_Token);

                if (i < columns.Length - 1)
                {
                    sb.Append(',');
                }
            }
            sb.Append(" FROM ");

            StringBuilder sbInside = new StringBuilder();
            sbInside.Append(identyColumn);
            sbInside.Append(" NOT IN (SELECT TOP ");
            sbInside.Append(skipCount);
            sbInside.Append(' ');
            sbInside.Append(identyColumn);
            sbInside.Append(" FROM ");
            sbInside.Append(where.ToString());
            sbInside.Append(")");

            //if (.Length == 0)
            //{
            //    cloneWhere.Sql = sbInside.ToString();
            //}
            //else
            //{
            //    cloneWhere.Sql = "(" + cloneWhere.Sql.ToString() + ") AND " + sbInside.ToString();
            //}

            //sb.Append(cloneWhere.ToString());

            return null;
        }

    }
}
