using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess.Oracle
{
    public class OracleDbProvider : DbProvider
    {
        private const char Parameter_Prefix = ':';

        private ISqlStatementFactory sqlStatementFactory = new OracleStatementFactory();

        public OracleDbProvider(string connectionString) : 
            base(connectionString, OracleClientFactory.Instance) 
        { 
        
        }

        public override void AdjustParameter(DbParameter param)
        {
            throw new NotImplementedException();
        }

        public override ISqlStatementFactory CreateStatementFactory()
        {
            return sqlStatementFactory;
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
            get 
            { 
                return ""; 
            }
        }

        public override string ParamPrefix
        {
            get 
            { 
                return Parameter_Prefix.ToString(); 
            }
        }

    }
}
