using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Data.Queries;

namespace EApp.Data.MySql
{
    public class MySqlWhereClauseBuilder<T> : WhereClauseBuilder<T> where T: class, new()
    {
        public MySqlWhereClauseBuilder(IObjectMappingResolver mappingResolver) : base(mappingResolver) { }

        protected internal override char ParameterChar
        {
            get 
            {
                return '?';
            }
        }
    }
}
