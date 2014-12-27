using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Repositories.SqlServer;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using EApp.Core.QueryPaging;

namespace Xpress.Chart.Repositories
{
    public class PostRepository : SqlServerRepository<Post>, IPostRepository
    {
        private const string whereByPostId = "post_id=@id";

        public PostRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { 

        }

        protected override void PersistAddedItem(Post entity)
        {
            DbGateway.Default.Insert("post", 
                                     new object[] { entity.Topic.Id, 
                                                    entity.Author.Id, 
                                                    entity.Content, 
                                                    entity.CreationDateTime }, 
                                     this.SqlServerTranscation);
        }

        protected override void PersistModifiedItem(Post entity)
        {
            DbGateway.Default.Update("post", 
                                     new string[] { "post_content" }, 
                                     new object[] { entity.Content },
                                     whereByPostId, 
                                     new object[]{ entity.Id }, 
                                     this.SqlServerTranscation);
        }

        protected override void PersistDeletedItem(Post entity)
        {
            DbGateway.Default.Delete("post", whereByPostId, new object[] { entity.Id }, this.SqlServerTranscation);
        }

        protected override Post DoFind(ISpecification<Post> specification)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Post> DoFindAll(Expression<Func<Post, bool>> expression)
        {
            throw new NotImplementedException();
        }

        protected override IPagingResult<Post> DoFindAll(Expression<Func<Post, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityQuerySqlById()
        {
            return "select * from post where post_id = @id";
        }

        protected override Post BuildEntityFromDataReader(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, AppendChildToEntity> BuildChildCallbacks()
        {
            Dictionary<string, AppendChildToEntity> childCallbacks = new Dictionary<string, AppendChildToEntity>();

            childCallbacks.Add("", AppendTopicToPost);
            childCallbacks.Add("", AppendAuthorToPost);

            return childCallbacks;
        }

        private void AppendTopicToPost(Post post, int topicId)
        { 
            
        }

        private void AppendAuthorToPost(Post post, int authorId)
        {

        }

    }
}
