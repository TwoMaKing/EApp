using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Data.Query;
using EApp.Repositories.SqlServer;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Repositories
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

        protected override IEnumerable<Post> DoFindAll(Expression<Func<Post, bool>> expression)
        {
            using (ISqlQuery sqlQuery = new SqlQuery())
            {
                IDataReader reader = sqlQuery.From("post")
                                             .InnerJoin("topic", "post_topic_id", "topic_id")
                                             .InnerJoin("[user]", "post_author_id", "user_id")
                                             .Select("post_id",
                                                     "post_topic_id",
                                                     "post_author_id",
                                                     "post_content",
                                                     "post_creation_datetime",
                                                     "topic_name",
                                                     "user_name")
                                             .ExecuteReader(DbGateway.Default);

                return this.BuildEntitiesFromDataReader(reader).AsQueryable().Where(expression);
            }
        }

        protected override IPagingResult<Post> DoFindAll(Expression<Func<Post, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityQuerySqlById()
        {
            using (ISqlQuery sqlQuery = new SqlQuery())
            {
                string queryByIdSql = sqlQuery.From("post")
                                              .InnerJoin("topic", "post_topic_id", "topic_id")
                                              .InnerJoin("[user]", "post_author_id", "user_id")
                                              .Equals("post_id", "?")
                                              .Select("post_id",
                                                      "post_topic_id",
                                                      "post_author_id",
                                                      "post_content",
                                                      "post_creation_datetime",
                                                      "topic_name",
                                                      "user_name")
                                              .SqlBuilder
                                              .GetQuerySql();

                return queryByIdSql;
            }
        }

        protected override Post BuildEntityFromDataReader(IDataReader dataReader)
        { 
            Post post = new Post();

            post.Id = Convertor.ConvertToInteger(dataReader["post_id"]).Value;
            post.Content = dataReader["post_content"].ToString().Trim();
            post.CreationDateTime = Convertor.ConvertToDateTime(dataReader["post_creation_datetime"]).Value;

            post.Topic = new Topic();
            post.Topic.Id = Convertor.ConvertToInteger(dataReader["post_topic_id"]).Value;
            post.Topic.Name = dataReader["topic_name"].ToString().Trim();
            post.Author = new User();
            post.Author.Id = Convertor.ConvertToInteger(dataReader["post_topic_id"]).Value;
            post.Author.Name = dataReader["user_name"].ToString();

            return post;
        }

        protected override Dictionary<string, AppendChildToEntity> BuildChildCallbacks()
        {
            Dictionary<string, AppendChildToEntity> childCallbacks = new Dictionary<string, AppendChildToEntity>();

            childCallbacks.Add("post_topic_id", AppendTopicToPost);
            childCallbacks.Add("post_author_id", AppendAuthorToPost);

            return childCallbacks;
        }

        private void AppendTopicToPost(Post post, int topicId)
        {
            ITopicRepository topicReposiotry = ServiceLocator.Instance.GetService<ITopicRepository>();

            Topic topic = topicReposiotry.FindByKey(topicId);

            post.Topic = topic;
        }

        private void AppendAuthorToPost(Post post, int authorId)
        {
            IUserRepository userReposiotry = ServiceLocator.Instance.GetService<IUserRepository>();

            User user = userReposiotry.FindByKey(authorId);

            post.Author = user;
        }

        public IEnumerable<Post> GetPostsPublishedByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
