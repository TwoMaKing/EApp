using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public sealed class SqlQueryUtils
    {
        private SqlQueryUtils() { }

        public static bool OrderByStartsWith(string orderByClause, string column) 
        {
            if (string.IsNullOrEmpty(orderByClause) ||
                string.IsNullOrWhiteSpace(orderByClause) ||
                string.IsNullOrEmpty(column) ||
                string.IsNullOrWhiteSpace(column))
            {
                return false;
            }

            return orderByClause.Trim().StartsWith(column.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string ReplaceDatabaseTokens(string sql, char leftToken, char rightToken, char paramPrefixToken, char wildcharToken, char wildsinglecharToken)
        {
            string retSql = sql;
            if (leftToken != '[')
            {
                retSql = retSql.Replace("[", leftToken.ToString());
            }
            if (rightToken != ']')
            {
                retSql = retSql.Replace("]", rightToken.ToString());
            }
            if (paramPrefixToken != '@')
            {
                retSql = retSql.Replace("@", paramPrefixToken.ToString());
            }
            if (wildcharToken != '%')
            {
                retSql = retSql.Replace("%", wildcharToken.ToString());
            }
            if (wildsinglecharToken != '_')
            {
                retSql = retSql.Replace("_", wildsinglecharToken.ToString());
            }
            return retSql;
        }

        public static void AppendColumnName(StringBuilder sqlBuilder, string columnName, char leftToken, char rightToken)
        {
            if (sqlBuilder == null) throw new ArgumentNullException("sb could not be null!");

            if (string.IsNullOrEmpty(columnName) ||
                string.IsNullOrWhiteSpace(columnName)) 
                throw new ArgumentNullException("columnName could not be null!");

            if (columnName.Contains("."))
            {
                string[] splittedColumnSections = columnName.Split('.');
                for (int i = 0; i < splittedColumnSections.Length; ++i)
                {
                    if (splittedColumnSections[i] == "*" || 
                        splittedColumnSections[i].Contains(leftToken) ||
                        splittedColumnSections[i].Contains(rightToken))
                    {
                        sqlBuilder.Append(splittedColumnSections[i]);
                    }
                    else
                    {
                        sqlBuilder.Append(leftToken);
                        sqlBuilder.Append(splittedColumnSections[i]);
                        sqlBuilder.Append(rightToken);
                    }

                    if (i < splittedColumnSections.Length - 1)
                    {
                        sqlBuilder.Append('.');
                    }
                }
            }
            else
            {
                if (columnName == "*" || columnName.Contains(leftToken) || columnName.Contains(rightToken))
                {
                    sqlBuilder.Append(columnName);
                }
                else
                {
                    sqlBuilder.Append(leftToken);
                    sqlBuilder.Append(columnName);
                    sqlBuilder.Append(rightToken);
                }
            }
        }

    }
}
