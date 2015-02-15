using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EApp.Dapper.SqlServer
{
    public class SqlServerDbDriver : DbDriver
    {
        public override void AdjustParameter(DbParameter parameter)
        {
            throw new NotImplementedException();
        }

        public override ISqlStatementFactory CreateStatementFactory()
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
                return '@'; 
            }
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
    }
}
