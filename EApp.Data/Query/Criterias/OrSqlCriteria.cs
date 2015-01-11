using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public class OrSqlCriteria : CompositeSqlCriteria, ICompositeSqlCriteria
    {
        public OrSqlCriteria(ISqlCriteria left, ISqlCriteria right) : base(left, right) { }

        public override string GetSqlCriteria()
        {
            return string.Format(" {0} or {1} ", this.Left.GetSqlCriteria(), this.Right.GetSqlCriteria());
        }
    }
}
