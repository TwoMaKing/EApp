using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EApp.Common.Query;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Query;
using EApp.Domain.Core.Repositories;
using EApp.Repositories.SQL;
using NUnit.Framework;
using Xpress.Chat.Commands;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Events;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;
using Xpress.Chat.ServiceContracts;

namespace EApp.Tests
{
    [TestFixture()]
    public class EAppRepositoryTests : TestBase
    {
        public EAppRepositoryTests() : base() 
        { 
            
        }

        [Test()]
        public void Test_PostRepository_FindAllOrderByWithoutPaging() 
        {
            PostQueryRequest request = new PostQueryRequest();
            request.TopicId = 1000;
            request.CreationDateTimeParam.CreationDateTimeOperator = Operator.LessThanEqual;
            request.CreationDateTimeParam.CreationDateTime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IRepository<Post> postRepository = repositoryContext.GetRepository<Post>();

                Expression<Func<Post, bool>> dateTimeExpression = (p) => true;

                //DateTime dt = request.CreationDateTimeParam.CreationDateTime;

                switch (request.CreationDateTimeParam.CreationDateTimeOperator)
                {
                    case Operator.LessThanEqual:
                        dateTimeExpression = p => p.CreationDateTime <= request.CreationDateTimeParam.CreationDateTime;
                        break;
                    case Operator.GreaterThanEqual:
                        dateTimeExpression = p => p.CreationDateTime >= request.CreationDateTimeParam.CreationDateTime;
                        break;
                    case Operator.Equal:
                        dateTimeExpression = p => p.CreationDateTime.Equals(request.CreationDateTimeParam.CreationDateTime);
                        break;
                }

                QueryBuilder<Post> postQueryBuilder = new QueryBuilder<Post>();

                //int topicId = request.TopicId;

                postQueryBuilder.And(p => p.TopicId == request.TopicId).And(dateTimeExpression);

                IEnumerable<Post> posts = postRepository.FindAll(postQueryBuilder.QueryPredicate);

                IList<PostDataObject> postDataObjects = new List<PostDataObject>();

                foreach (Post post in posts)
                {
                    var postDataObject = new PostDataObject();
                    postDataObject.MapFrom(post);

                    postDataObjects.Add(postDataObject);
                }

                Assert.AreEqual(5, postDataObjects.Count);
            }
        
        }

    }
}
