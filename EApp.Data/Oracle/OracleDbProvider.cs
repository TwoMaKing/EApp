using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using EApp.Data.Mapping;
using EApp.Data.Queries;

namespace EApp.Data.Oracle
{
    public class OracleDbProvider : DbProvider
    {
        private const char Parameter_Prefix = ':';

        private ISqlStatementFactory sqlStatementFactory;

        public OracleDbProvider(string connectionString) : 
            base(connectionString, OracleClientFactory.Instance) 
        {
            this.sqlStatementFactory = new OracleStatementFactory(this);
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

        public override char ParameterPrefix
        {
            get 
            { 
                return Parameter_Prefix; 
            }
        }

        public override char ParameterLeftToken
        {
            get 
            { 
                return '\"';
            }
        }

        public override char ParameterRightToken
        {
            get 
            { 
                return '\"'; 
            }
        }

        public override char WildCharToken
        {
            get 
            { 
                return '%';
            }
        }

        public override char WildSingleCharToken
        {
            get 
            { 
                return '_';
            }
        }

        public override WhereClauseBuilder<T> CreateWhereClauseBuilder<T>()
        {
            return new OracleWhereClauseBuilder<T>();
        }
    }
}
