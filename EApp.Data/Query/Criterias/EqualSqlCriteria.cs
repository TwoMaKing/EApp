using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public class EqualSqlCriteria : SqlCriteria
    {
        public EqualSqlCriteria(string column) : base(column) { }

        public override string GetSqlCriteria()
        {
            return this.Column + " = @" + this.Column;
        }
    }
}
