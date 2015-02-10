using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Data.Mapping;
using EApp.Data.Queries;

namespace EApp.Data.MySql
{
    public class MySqlWhereClauseBuilder<T> : WhereClauseBuilder<T> where T: class, new()
    {
        public MySqlWhereClauseBuilder(IObjectMappingResolver mappingResolver) : base(mappingResolver) { }

        public MySqlWhereClauseBuilder() : base() { }

        protected internal override char ParameterChar
        {
            get 
            {
                return '?';
            }
        }
    }
}
