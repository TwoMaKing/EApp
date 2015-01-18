using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public class EndsWithSqlCriteria : LikeSqlCriteria
    {
        public EndsWithSqlCriteria(DbProvider dbProvider, string column) : base(dbProvider, column) { }

        public override string GetSqlCriteria()
        {
            return string.Format("{0} {1} {2}", this.BuildedDbColumn, this.GetOperatorChar(), " " + this.ParameterColumn + "%");
        }
    }
}
