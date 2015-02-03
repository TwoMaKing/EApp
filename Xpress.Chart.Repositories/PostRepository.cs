using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Data.Queries;
using EApp.Domain.Core.Repositories;
using EApp.Repositories.SQL;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Repositories
{
    public class PostRepository : SqlRepository<Post>, IPostRepository
    {
        public PostRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { 

        }

        public IEnumerable<Post> GetPostsPublishedByUser(User user)
        {
            throw new NotImplementedException();
        }

        protected override Post DoFind(ISpecification<Post> specification)
        {
            throw new NotImplementedException();
        }

        protected override string GetAggregateRootQuerySqlById()
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

        protected override Post BuildAggregateRootFromDataReader(IDataReader dataReader)
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

        protected override Dictionary<string, AppendChildToAggregateRoot> BuildChildCallbacks()
        {
            Dictionary<string, AppendChildToAggregateRoot> childCallbacks = new Dictionary<string, AppendChildToAggregateRoot>();

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

        protected override void DoPersistAddedItems(IEnumerable<Post> aggregateRoots)
        {
            foreach (Post post in aggregateRoots)
            {
                this.SqlRepositoryContext.Insert("post",
                                                 new object[] { post.Topic.Id, 
                                                                post.Author.Id, 
                                                                post.Content, 
                                                                post.CreationDateTime });
            }
        }

        protected override void DoPersistModifiedItems(IEnumerable<Post> aggregateRoots)
        {
            foreach (Post post in aggregateRoots)
            {
                this.SqlRepositoryContext.Update("post",
                                                 new string[] { "post_topic_id", "post_author_id", "content" },
                                                 new object[] { post.Topic.Id, 
                                                                post.Author.Id, 
                                                                post.Content },
                                                 "post_id=@post_id",
                                                 new object[] { post.Id });
            }
        }

        protected override void DoPersistDeletedItems(IEnumerable<Post> aggregateRoots)
        {
            foreach (Post post in aggregateRoots)
            {
                this.SqlRepositoryContext.Delete("post",
                                                 "post_id=@post_id",
                                                 new object[] { post.Id });
            }
        }
    }
}
