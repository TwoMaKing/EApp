using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess.Oracle
{
    public class OracleStatementFactory : ISqlStatementFactory
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

        public string CreateSelectStatement(string tableName, string where, string orderBy, params string[] includedColumns)
        {
            throw new NotImplementedException();
        }
    }
}
