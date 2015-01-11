using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query.Criterias
{
    public abstract class SqlCriteria : ISqlCriteria
    {
        protected SqlCriteria() { }

        protected SqlCriteria(string column)
        {
            this.Column = column;
        }

        protected string Column { get; set; }

        public abstract string GetSqlCriteria();

    }
}
