using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.DataAccess;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Repository;
using EApp.Infrastructure.UnitOfWork;
using EApp.Repositories.SqlServer;
using Xpress.Chart.Domain;

namespace Xpress.Chart.Repositories
{
    public class PostRepository : SqlServerRepository<Post>
    {
        public PostRepository(ISqlServerRepositoryContext repositoryContext) : base(repositoryContext)
        { 
            
        }

        protected override void PersistAddedItem(Post entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistModifiedItem(Post entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistDeletedItem(Post entity)
        {
            throw new NotImplementedException();
        }

        protected override Post DoFindByKey(Guid idOrKey)
        {
            throw new NotImplementedException();
        }

        protected override Post DoFind(ISpecification<Post> specification)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Post> DoFindAll(Expression<Func<Post, bool>> expression)
        {
            throw new NotImplementedException();
        }

        protected override EApp.Core.QueryPaging.IPagingResult<Post> DoFindAll(Expression<Func<Post, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
