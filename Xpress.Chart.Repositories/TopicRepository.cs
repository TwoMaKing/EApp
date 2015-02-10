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
    public class TopicRepository : SqlRepository<Topic>, ITopicRepository
    {
        private const string whereById = "topic_id=@id";

        public TopicRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override string GetAggregateRootQuerySqlById()
        {
            return "select * from topic where topic_id = @id";
        }

        protected override Topic BuildAggregateRootFromDataReader(IDataReader dataReader)
        {
            Topic topic = new Topic();

            topic.Id = Convertor.ConvertToInteger(dataReader["topic_id"]).Value;
            topic.Name = dataReader["topic_name"].ToString();
            topic.Summary = dataReader["topic_desc"].ToString();

            return topic;
        }

        protected override Dictionary<string, AppendChildToAggregateRoot> BuildChildCallbacks()
        {
            return null;
        }

        protected override void DoPersistAddedItems(IEnumerable<Topic> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistModifiedItems(IEnumerable<Topic> aggregateRoots)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistDeletedItems(IEnumerable<Topic> aggregateRoots)
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
