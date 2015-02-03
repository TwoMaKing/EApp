using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
    public class CommentRepository : SqlRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override string GetAggregateRootQuerySqlById()
        {
            throw new NotImplementedException();
        }

        protected override Comment BuildAggregateRootFromDataReader(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, AppendChildToAggregateRoot> BuildChildCallbacks()
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistAddedItems(IEnumerable<Comment> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistModifiedItems(IEnumerable<Comment> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistDeletedItems(IEnumerable<Comment> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByPost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
