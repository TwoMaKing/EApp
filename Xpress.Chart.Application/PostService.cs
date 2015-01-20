using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Events;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;
using Xpress.Chart.Repositories;
using Xpress.Chart.ServiceContracts;

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

        /// <summary>
        /// 这是最普通的DDD 应用层 (application service) 创建一个Post并保存到数据库中发布。
        /// 没有考虑用异步来保存数据库 或者 用 异步Domain Event handler 来保存Post到数据, 
        /// 用异步保存的同时 Post 的传输对象 PostDataObject返回 (加快速度,不需要等待数据库保存完成再返回)
        /// 也没有考虑将Post 送至 消息队列 由 另一个处理消息队列的系统来保存Post.
        /// </summary>
        private void PublishPostWithCommonSync(PostDataObject postDataObject)
        {
            //Topic topic = postDataObject.MapTo().Topic; //this.topicRepository.FindByKey(postDataObject.TopicId);

            //User author = postDataObject.MapTo().Author; //this.userRepository.FindByKey(postDataObject.AuthorId);

            Post post = postDataObject.MapTo();

            //Post post = Post.Create(topic, author, postDataObject.Content);

            this.postRepository.Add(post);

            this.RepositoryContext.Commit();

            this.RepositoryContext.Dispose();
        }

        /// <summary>
        /// 用 异步Domain Event handler 来保存Post到数据, 
        /// 用异步保存的同时 Post 的传输对象 PostDataObject返回 (加快速度,不需要等待数据库保存完成再返回)
        /// </summary>
        private void PublishPostWithAsyncDomainEventHandler(PostDataObject postDataObject)
        {
            //Topic topic = postDataObject.MapTo().Topic; //this.topicRepository.FindByKey(postDataObject.TopicId);

            //User author = postDataObject.MapTo().Author; //this.userRepository.FindByKey(postDataObject.AuthorId);

            //Post post = Post.Create(topic, author, postDataObject.Content);

            Post post = postDataObject.MapTo();

            //用 多线程 异步 并行 Domain Event Handler 来 保存
            DomainEventAggregator.Instance.Publish<PostDomainEvent>
                (
                    new PostDomainEvent(post)
                    {
                        Post = post
                    }
                );
        }

        /// <summary>
        /// 将 Post 送入 Rabbit 消息 队列，由另一个处理消息队列的系统来 进行Post的更新操作！
        /// </summary>
        private void PublishPostWithRabbitMessageQueue(PostDataObject postDataObject)
        {
            //Topic topic = postDataObject.MapTo().Topic; //this.topicRepository.FindByKey(postDataObject.TopicId);

            //User author = postDataObject.MapTo().Author; //this.userRepository.FindByKey(postDataObject.AuthorId);

            //Post post = Post.Create(topic, author, postDataObject.Content);

            Post post = postDataObject.MapTo();

            // 领域模型业务的真正实现，而不是一个 贫血的模型 是 一个充血的模型.
            post.Publish();
        }

        public void PublishPost(PostDataObject post)
        {
            this.PublishPostWithAsyncDomainEventHandler(post);
        }

        public IEnumerable<PostDataObject> GetPosts(QueryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
