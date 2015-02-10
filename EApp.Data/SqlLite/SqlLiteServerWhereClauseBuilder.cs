using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Data.Mapping;
using EApp.Data.Queries;

namespace EApp.Data.SqlLite
{
    public class SqlLiteServerWhereClauseBuilder<T> : WhereClauseBuilder<T> where T : class, new()
    {
        public SqlLiteServerWhereClauseBuilder(IObjectMappingResolver mappingResolver) : base(mappingResolver) { }

        public SqlLiteServerWhereClauseBuilder() : base() { }

        protected internal override char ParameterChar
        {
            get 
            {
                return '?'; 
            }
        }
    }
}
