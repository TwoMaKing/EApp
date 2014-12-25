using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Util;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Core.QueryPaging;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Repositories.SqlServer;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;

namespace Xpress.Chart.Repositories
{
    public class UserRepository : SqlServerRepository<User>, IUserRepository
    {
        private const string whereById = "user_id=@id";

        public UserRepository(ISqlServerRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override void PersistAddedItem(User entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistModifiedItem(User entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistDeletedItem(User entity)
        {
            throw new NotImplementedException();
        }

        //protected override User DoFindByKey(int id)
        //{
        //    User user = null;

        //    using (IDataReader reader = DbGateway.Default.ExecuteReader("select * from user where " + whereById, new object[] { id }))
        //    {
        //        if (reader.Read())
        //        {
        //            user = new User();

        //            user.Id = Convertor.ConvertToInteger(reader["user_id"]).Value;
        //            user.Name = reader["user_name"].ToString();
        //            user.NickName = reader["user_nick_name"].ToString();
        //            user.Email = reader["user_email"].ToString();
        //            user.Password = reader["user_password"].ToString();
        //        }

        //        reader.Close();
        //        reader.Dispose();
        //    }

        //    return user;
        //}

        protected override User DoFind(ISpecification<User> specification)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<User> DoFindAll(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        protected override IPagingResult<User> DoFindAll(Expression<Func<User, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityQuerySqlById()
        {
            return "select * from user where user_id = @id";
        }

        protected override User BuildEntityFromDataReader(IDataReader dataReader)
        {
            User user = new User();

            user.Id = Convertor.ConvertToInteger(dataReader["user_id"]).Value;
            user.Name = dataReader["user_name"].ToString();
            user.NickName = dataReader["user_nick_name"].ToString();
            user.Email = dataReader["user_email"].ToString();
            user.Password = dataReader["user_password"].ToString();

            return user;
        }

        protected override Dictionary<string, SqlServerRepository<User>.AppendChildToEntity> BuildChildCallbacks()
        {
            return null;
        }

    }
}
