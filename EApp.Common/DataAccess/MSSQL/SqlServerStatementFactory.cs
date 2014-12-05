using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess.MSSQL
{
    public class SqlServerStatementFactory : ISqlStatementFactory
    {
        public string CreateInsertStatement(string tableName, string[] includedColumns)
        {
            throw new NotImplementedException();
        }

        public string CreateUpdateStatement(string tableName, string where, string[] includedColumns)
        {
            throw new NotImplementedException();
        }

        public string CreateDeleteStatement(string tableName, string where)
        {
            throw new NotImplementedException();
        }

        public string CreateSelectStatement(string tableName, string where, string orderBy, params string[] includeColumns)
        {
            throw new NotImplementedException();
        }
    }
}
