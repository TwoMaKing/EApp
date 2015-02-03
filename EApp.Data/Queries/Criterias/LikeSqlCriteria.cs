using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Queries.Criterias
{
    public class LikeSqlCriteria : OperatorSqlCriteria
    {
        public LikeSqlCriteria(DbProvider dbProvider, string column)
            : base(dbProvider, column)
        { 
            
        }

        protected override string GetOperatorChar()
        {
            return "LIKE";
        }

    }
}
