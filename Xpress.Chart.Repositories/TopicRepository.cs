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
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Repositories.SqlServer;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Repositories
{
    public class TopicRepository : SqlServerRepository<Topic>, ITopicRepository
    {
        private const string whereById = "topic_id=@id";

        public TopicRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override void PersistAddedItem(Topic entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistModifiedItem(Topic entity)
        {
            throw new NotImplementedException();
        }

        protected override void PersistDeletedItem(Topic entity)
        {
            throw new NotImplementedException();
        }

        protected override Topic DoFind(ISpecification<Topic> specification)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Topic> DoFindAll(Expression<Func<Topic, bool>> expression)
        {
            throw new NotImplementedException();
        }

        protected override IPagingResult<Topic> DoFindAll(Expression<Func<Topic, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityQuerySqlById()
        {
            return "select * from topic where topic_id = @id";
        }

        protected override Topic BuildEntityFromDataReader(IDataReader dataReader)
        {
            Topic topic = new Topic();

            topic.Id = Convertor.ConvertToInteger(dataReader["topic_id"]).Value;
            topic.Name = dataReader["topic_name"].ToString();
            topic.Summary = dataReader["topic_desc"].ToString();

            return topic;
        }

        protected override Dictionary<string, SqlServerRepository<Topic>.AppendChildToEntity> BuildChildCallbacks()
        {
            return null;
        }
    }
}
