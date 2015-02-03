using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Data.Queries
{
    public static class SqlBuilderExtension
    {
        public static ISqlBuilder Where<T>(this ISqlBuilder sqlBuilder, Expression<Func<T, bool>> predicate)
        {
            
            return sqlBuilder;
        }
    }
}
