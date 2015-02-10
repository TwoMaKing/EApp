using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EApp.Common.Query;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Query;
using EApp.Domain.Core.Repositories;
using Xpress.Chat.Commands;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Events;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;
using Xpress.Chat.ServiceContracts;
namespace Xpress.Chat.Application
{
    public class PostQueryService : DisposableObject, IQueryService
    {
        public IEnumerable<PostDataObject> GetPosts()
        {
            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IRepository<Post> postRepository = repositoryContext.GetRepository<Post>();

                IEnumerable<Post> posts = postRepository.FindAll();

                IList<PostDataObject> postDataObjects = new List<PostDataObject>();

                foreach (Post post in posts)
                {
                    PostDataObject postDataObject = new PostDataObject();

                    postDataObject.MapFrom(post);

                    postDataObjects.Add(postDataObject);
                }

                return postDataObjects;
            }
        }

        public IEnumerable<DataObjects.PostDataObject> GetPostsByQueryRequest(PostQueryRequest request)
        {
            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IRepository<Post> postRepository = repositoryContext.GetRepository<Post>();

                Expression<Func<Post, bool>> dateTimeExpression = (p) => true;

                switch (request.CreationDateTimeParam.CreationDateTimeOperator)
                {
                    case Operator.LessThanEqual:
                        dateTimeExpression = p => p.CreationDateTime <= request.CreationDateTimeParam.CreationDateTime;
                        break;
                    case Operator.GreaterThanEqual:
                        dateTimeExpression = p => p.CreationDateTime >= request.CreationDateTimeParam.CreationDateTime;
                        break;
                    case Operator.Equal:
                    default:
                        dateTimeExpression = p => p.CreationDateTime.Equals(request.CreationDateTimeParam.CreationDateTime);
                        break;
                }

                QueryBuilder<Post> postQueryBuilder = new QueryBuilder<Post>();

                postQueryBuilder.And(p => p.TopicId == request.TopicId).And(dateTimeExpression);

                IEnumerable<Post> posts = postRepository.FindAll(postQueryBuilder.QueryPredicate);

                IList<PostDataObject> postDataObjects = new List<PostDataObject>();

                foreach (Post post in posts)
                {
                    var postDataObject = new PostDataObject();
                    postDataObject.MapFrom(post);

                    postDataObjects.Add(postDataObject);
                }

                return postDataObjects;
            }
        }

        protected override void Dispose(bool disposing)
        {
            //
        }
    }
}
