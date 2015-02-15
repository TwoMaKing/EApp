using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.Query;
using EApp.Data.Queries;
using EApp.Domain.Core.Repositories;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public interface ISqlRepositoryContext : IRepositoryContext
    {
        DbConnection CreateConnection();

        void CloseConnection(DbConnection connection);

        WhereClauseBuildResult GetWhereClauseSql<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> predicate) where TAggregateRoot : class, new();

        string GetOrderByClauseSql<TAggregateRoot>(Expression<Func<TAggregateRoot, dynamic>> predicate) where TAggregateRoot : class, new();

        IDataReader Select(string querySql, object[] whereParamValues = null, DbTransaction transaction = null);

        IDataReader Select(string table, string[] columns, DbTransaction transaction = null);

        IDataReader Select(string table, string[] columns, string where, object[] whereParamValues, DbTransaction transaction = null);

        IDataReader Select(string table, string[] columns, string where, object[] whereParamValues, string orderBy, DbTransaction transaction = null);

        IDataReader Select(string table, string[] columns, string where, object[] whereParamValues, string orderBy, int pageNumber, int pageSize, string identityColumn, bool identityColumnIsNumber = true, DbTransaction transaction = null);

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
