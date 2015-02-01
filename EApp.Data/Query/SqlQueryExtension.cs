using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Data.Query
{
    public static class SqlQueryExtension
    {
        public static IDataReader ExecuteReader(this ISqlQuery sqlQuery, DbGateway dbGateway) 
        {
            string commandSql = sqlQuery.SqlBuilder.GetQuerySql();

            object[] paramValues = sqlQuery.SqlBuilder.GetParameters().Values.ToArray();

            return dbGateway.ExecuteReader(commandSql, paramValues);
        }

        public static IDataReader ExecuteReader(this ISqlQuery sqlQuery, DbGateway dbGateway, int pageNumber, int pageSize)
        {
            string commandSql = sqlQuery.SqlBuilder.GetQuerySql();

            object[] paramValues = sqlQuery.SqlBuilder.GetParameters().Values.ToArray();

            return dbGateway.ExecuteReader(commandSql, paramValues);
        }

        public static ISqlQuery Where<T>(this ISqlQuery sqlQuery, Expression<Func<T, bool>> predicate)
        {

            return sqlQuery;
        }

    }
}
