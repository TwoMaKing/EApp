using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public class AndSqlCriteria : CompositeSqlCriteria, ICompositeSqlCriteria
    {
        public AndSqlCriteria(ISqlCriteria left, ISqlCriteria right) : base(left, right) { }

        public override string GetSqlCriteria()
        {
            return string.Format(" {0} {1} {2} ", this.Left.GetSqlCriteria(), this.GetOperatorChar(), this.Right.GetSqlCriteria());
        }

        protected override string GetOperatorChar()
        {
            return "AND";
        }
    }
}
