using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Repositories.SqlServer;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Repositories
{
    public class CommentRepository : SqlServerRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override void PersistAddedItem(Comment entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistModifiedItem(Comment entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistDeletedItem(Comment entity)
        {
            throw new NotImplementedException();
        }

        protected override Comment DoFindByKey(int id)
        {
            throw new NotImplementedException();
        }

        protected override Comment DoFind(ISpecification<Comment> specification)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Comment> DoFindAll(Expression<Func<Comment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        protected override IPagingResult<Comment> DoFindAll(Expression<Func<Comment, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityQuerySqlById()
        {
            throw new NotImplementedException();
        }

        protected override Comment BuildEntityFromDataReader(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, SqlServerRepository<Comment>.AppendChildToEntity> BuildChildCallbacks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByPost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
