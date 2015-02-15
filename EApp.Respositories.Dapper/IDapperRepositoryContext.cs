using Dappers;
using EApp.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EApp.Respositories.Dapper
{
    public interface IDapperRepositoryContext : IRepositoryContext
    {
        IDbConnection CreateConnection();

        void CloseConnection(IDbConnection connection);

        T Query<T>(string querySql, object @params);

        void Insert(string insertSql, object obj);

        void Update(string updateSql, object obj);

        void Delete(string deleteSql, object obj);

    }
}
