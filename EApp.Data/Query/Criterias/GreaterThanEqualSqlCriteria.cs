using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public class GreaterThanEqualSqlCriteria : OperatorSqlCriteria
    {
        public GreaterThanEqualSqlCriteria(DbProvider dbProvider, string column) : base(dbProvider, column) { }

        protected override string GetOperatorChar()
        {
            return ">=";
        }

    }
}
