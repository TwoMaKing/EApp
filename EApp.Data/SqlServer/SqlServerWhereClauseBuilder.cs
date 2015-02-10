using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Data.Mapping;
using EApp.Data.Queries;

namespace EApp.Data.SqlServer
{
    public class SqlServerWhereClauseBuilder<T> : WhereClauseBuilder<T> where T : class, new()
    {
        public SqlServerWhereClauseBuilder(IObjectMappingResolver mappingResolver) : base(mappingResolver) { }

        public SqlServerWhereClauseBuilder() : base() { }

        protected internal override char ParameterChar
        {
            get 
            {
                return '@';
            }
        }
    }
}
