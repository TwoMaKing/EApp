using System;
using System.Linq.Expressions;

namespace EApp.Data.Queries
{
    public interface IOrderByClauseBuilder<T> where T : class, new()
    {
        string BuildOrderByClause(Expression<Func<T, dynamic>> expression);
    }
}
