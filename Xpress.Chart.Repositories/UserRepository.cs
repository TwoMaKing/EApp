using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Util;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Domain.Core.Repositories;
using EApp.Repositories.SQL;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Repositories
{
    public class UserRepository : SqlRepository<User>, IUserRepository
    {
        private const string whereById = "user_id=@id";

        public UserRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }


        protected override User DoFind(ISpecification<User> specification)
        {
            throw new NotImplementedException();
        }

        protected override string GetAggregateRootQuerySqlById()
        {
            return "select * from [user] where [user_id] = @id";
        }

        protected override User BuildAggregateRootFromDataReader(IDataReader dataReader)
        {
            User user = new User();

            user.Id = Convertor.ConvertToInteger(dataReader["user_id"]).Value;
            user.Name = dataReader["user_name"].ToString();
            user.NickName = dataReader["user_nick_name"].ToString();
            user.Email = dataReader["user_email"].ToString();
            user.Password = dataReader["user_password"].ToString();

            return user;
        }

        protected override Dictionary<string, AppendChildToAggregateRoot> BuildChildCallbacks()
        {
            return null;
        }

        protected override void DoPersistAddedItems(IEnumerable<User> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistModifiedItems(IEnumerable<User> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistDeletedItems(IEnumerable<User> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override string GetFromTableSqlByFindAll()
        {
            throw new NotImplementedException();
        }

        protected override string[] GetSelectColumnsByFindAll()
        {
            throw new NotImplementedException();
        }
    }
}
