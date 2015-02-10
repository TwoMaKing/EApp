using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Data.Oracle
{
    public class OracleStatementFactory : SqlStatementFactory, ISqlStatementFactory
    {
        public OracleStatementFactory(DbProvider dbProvider) : base(dbProvider) { }

        public override string CreateInsertStatement(string tableName, string[] includedColumns)
        {
            throw new NotImplementedException();
        }

        public override string CreateUpdateStatement(string tableName, string where, string[] includedColumns)
        {
            throw new NotImplementedException();
        }

        public override string CreateDeleteStatement(string tableName, string where)
        {
            throw new NotImplementedException();
        }

        public override string CreateSelectStatement(string tableName, string where, string orderBy, params string[] includedColumns)
        {
            throw new NotImplementedException();
        }


        protected override string CreateSelectTopStatement(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount)
        {
            throw new NotImplementedException();
        }

        protected override string CreateSelectRangeStatementForSortedRows(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identityColumn, bool isIdentityColumnDesc)
        {
            throw new NotImplementedException();
        }

        protected override string CreateSelectRangeStatementForUnsortedRows(string tableName, string where, string[] columns, string orderBy, string groupBy, int topCount, int skipCount, string identyColumn)
        {
            throw new NotImplementedException();
        }
    }
}
