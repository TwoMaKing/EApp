using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public interface ISqlStatementFactory
    {

        /// <summary>
        ///  Creates the insert statement
        /// </summary>
        string CreateInsertSqlStatement(string tableName, string[] columns = null);

        /// <summary>
        /// Creates the update statement
        /// </summary>
        string CreateUpdateSqlStatement(string tableName, string[] columns, string whereSql);

        /// <summary>
        /// Creates the delete statement
        /// </summary>
        string CreateDeleteSqlStatement(string tableName, string whereSql);

        /// <summary>
        /// Creates the select statement
        /// </summary>
        string CreateSelectSqlStatement(string tableNames,
                                        string whereSql,
                                        string orderBy,
                                        params string[] columns);

        /// <summary>
        /// Creates the select statement for paging
        /// </summary>
        string CreateSelectRangeSqlStatement(string tableNames, 
                                             string whereSql, 
                                             string orderBy,
                                             string groupBy,
                                             int topCount, 
                                             int skipCount, 
                                             string identityColumn, 
                                             bool identityColumnIsNumber = true,
                                             params string[] columns);
    }
}
