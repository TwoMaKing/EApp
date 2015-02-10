using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using EApp.Data.Mapping;
using EApp.Data.Queries;

namespace EApp.Data.SqlLite
{
    public class SqlLiteDbProvider : DbProvider
    {
        private const char Parameter_Prefix = '?';

        private ISqlStatementFactory sqlStatementFactory;

        public SqlLiteDbProvider(string connectionString) : 
            base(connectionString, System.Data.SQLite.SQLiteFactory.Instance) 
        {
            this.sqlStatementFactory = new SqlLiteStatementFactory(this);
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

        public override char ParameterPrefix
        {
            get { throw new NotImplementedException(); }
        }

        public override char ParameterLeftToken
        {
            get 
            { 
                return '['; 
            }
        }

        public override char ParameterRightToken
        {
            get 
            { 
                return ']'; 
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
            return new SqlLiteServerWhereClauseBuilder<T>();
        }
    }
}
