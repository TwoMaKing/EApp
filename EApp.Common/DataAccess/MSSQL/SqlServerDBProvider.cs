using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess.MSSQL
{
    public class SqlServerDBProvider : DbProvider
    {
        public SqlServerDBProvider(string connectionString) : 
            base(connectionString, System.Data.SqlClient.SqlClientFactory.Instance)
        { 
        
        }

        public override void AdjustParameter(System.Data.Common.DbParameter param)
        {
            throw new NotImplementedException();
        }

        public override IStatementFactory CreateStatementFactory()
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

        public override string LeftToken
        {
            get { throw new NotImplementedException(); }
        }

        public override string RightToken
        {
            get { throw new NotImplementedException(); }
        }
    }
}
