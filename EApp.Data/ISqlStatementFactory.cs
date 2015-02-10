using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Data.Queries;

namespace EApp.Data
{
    public interface ISqlStatementFactory
    {

        /// <summary>
        ///  Creates the insert statement
        /// </summary>
        string CreateInsertStatement(string tableName, string[] includedColumns);


        /// <summary>
        /// Creates the update statement
        /// </summary>
        string CreateUpdateStatement(string tableName, string where, string[] includedColumns);


        /// <summary>
        /// Creates the delete statement
        /// </summary>
        string CreateDeleteStatement(string tableName, string where);


        /// <summary>
        /// Creates the select statement
        /// </summary>
        string CreateSelectStatement(string tableName,
                                     string where,
                                     string orderBy,
                                     params string[] includedColumns);

        /// <summary>
        /// Creates the select statement for paging
        /// </summary>
        string CreateSelectRangeStatement(string tableName, 
                                          string where, 
                                          string orderBy,
                                          int topCount, 
                                          int skipCount, 
                                          string identityColumn, 
                                          bool identityColumnIsNumber = true,
                                          string groupBy = null,
                                          params string[] includedColumns);
    }
}
