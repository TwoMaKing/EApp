using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.Query;
using EApp.Domain.Core.Repositories;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public interface ISqlRepositoryContext : IRepositoryContext
    {
        IEnumerable<T> Select<T>();

        IEnumerable<T> Select<T>(Expression<Func<T, bool>> expression);

        IEnumerable<T> Select<T>(Expression<Func<T, bool>> expression, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder);

        IEnumerable<T> Select<T>(Expression<Func<T, bool>> expression, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);

        void Insert(string table, string[] columns, object[] values);

        void Insert(string table, object[] values);

        void Update(string table,
                    string[] columns,
                    object[] values);

        void Update(string table,
                    string[] columns,
                    object[] values,
                    string whereSql,
                    object[] whereParamValues);

        void Delete(string table);

        void Delete(string table, string whereSql, object[] whereParamValues);

        void ExecuteNonQuery(string commandText, object[] paramValues = null);
    }
}
