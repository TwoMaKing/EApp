using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.LightBus;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chat.Commands;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Events;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;
using Xpress.Chat.ServiceContracts;

namespace Xpress.Chat.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class QueryService : IQueryService
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
                    var postDataObject = new PostDataObject();
                    postDataObject.MapFrom(post);

                    postDataObjects.Add(postDataObject);
                }

                return postDataObjects;
            }
        }

        public IEnumerable<PostDataObject> GetPostsByQueryRequest(QueryRequest request)
        {
            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IRepository<Post> postRepository = repositoryContext.GetRepository<Post>();

                IEnumerable<Post> posts = postRepository.FindAll();

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
    }
}
