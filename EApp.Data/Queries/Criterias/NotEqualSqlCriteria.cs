using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Queries.Criterias
{
    public class NotEqualSqlCriteria : OperatorSqlCriteria
    {
        public NotEqualSqlCriteria(DbProvider dbProvider, string column) : base(dbProvider, column) { }

        protected override string GetOperatorChar()
        {
            return "!=";
        }
    }
}
