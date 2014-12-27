using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;
using Xpress.Chart.Repositories;
using Xpress.Chart.ServiceContracts;
using System.Threading.Tasks;

namespace Xpress.Chart.Application
{
    public class PostService : ApplicationService, IPostService
    {
        private ITopicRepository topicRepository;

        private IUserRepository userRepository;

        private IPostRepository postRepository;

        private IDomainService domainService;

        public PostService(IRepositoryContext repositoryContext,
                           ITopicRepository topicRepository,
                           IUserRepository userRepository,
                           IPostRepository postRepository,
                           IDomainService domainService) : base(repositoryContext)
        {
            this.topicRepository = topicRepository;
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.domainService = domainService;
        }

        public IEnumerable<PostDataObject> GetPosts(int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        public PostDataObject PublishPost(int topicId, int authorId, string content)
        {
            Topic topic = this.topicRepository.FindByKey(topicId);

            User author = this.userRepository.FindByKey(authorId);

            Post post = Post.Create(topic, author, content);

            this.postRepository.Add(post);

            this.RepositoryContext.Commit();

            this.RepositoryContext.Dispose();

            return new PostDataObject() { TopicName = topic.Name, 
                                          AuthorName = author.NickName, 
                                          Content = post.Content, 
                                          CreationDateTime = post.CreationDateTime };
        }
    }
}
