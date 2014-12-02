using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess
{
    public interface IStatementFactory
    {

        /// <summary>
        ///  Creates the insert statement
        /// </summary>
        string CreateInsertStatement(string tableName, params string[] includeColumns);


        /// <summary>
        /// Creates the update statement
        /// </summary>
        string CreateUpdateStatement(string tableName, string where, params string[] includeColumns);


        /// <summary>
        /// Creates the delete statement
        /// </summary>
        string CreateDeleteStatement(string tableName, string where);


        /// <summary>
        /// Creates the delete statement
        /// </summary>
        string CreateSelectStatement(string tableName,
                                     string where,
                                     string orderBy,
                                     params string[] includeColumns);
    }
}
