using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace EApp.Data.SqlLite
{
    public class SqlLiteDbProvider : DbProvider
    {
        private const char Parameter_Prefix = '?';

        private ISqlStatementFactory sqlStatementFactory = new SqlLiteStatementFactory();

        public SqlLiteDbProvider(string connectionString) : 
            base(connectionString, System.Data.SQLite.SQLiteFactory.Instance) 
        { 
            
        }

        public override void AdjustParameter(System.Data.Common.DbParameter param)
        {
            throw new NotImplementedException();
        }

        public override ISqlStatementFactory CreateStatementFactory()
        {
            throw new NotImplementedException();
        }

        public override string[] DiscoverParams(string sql)
        {
            throw new NotImplementedException();
        }

        public override string BuildParameterName(string name)
        {
            throw new NotImplementedException();
        }

        public override string BuildColumnName(string name)
        {
            throw new NotImplementedException();
        }

        public override string SelectLastInsertedRowAutoIDStatement
        {
            get { throw new NotImplementedException(); }
        }

        public override string ParamPrefix
        {
            get { throw new NotImplementedException(); }
        }
    }
}
