using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
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

        public IEnumerable<PostDataObject> GetPosts(int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }


        public PostDataObject PublishPost(int topicId, int authorId, string content)
        {
            //还没调试，如果不行就用 PublishPostWithCommonSync(topicId, authorId, content);

            return PublishPostWithAsyncDomainEventHandler(topicId, authorId, content); 
                
                //PublishPostWithCommonSync(topicId, authorId, content);
        }

        /// <summary>
        /// 这是最普通的DDD 应用层 (application service) 创建一个Post并保存到数据库中发布。
        /// 没有考虑用异步来保存数据库 或者 用 异步Domain Event handler 来保存Post到数据, 
        /// 用异步保存的同时 Post 的传输对象 PostDataObject返回 (加快速度,不需要等待数据库保存完成再返回)
        /// 也没有考虑将Post 送至 消息队列 由 另一个处理消息队列的系统来保存Post.
        /// </summary>
        private PostDataObject PublishPostWithCommonSync(int topicId, int authorId, string content)
        {
            Topic topic = this.topicRepository.FindByKey(topicId);

            User author = this.userRepository.FindByKey(authorId);

            Post post = Post.Create(topic, author, content);

            this.postRepository.Add(post);

            this.RepositoryContext.Commit();

            this.RepositoryContext.Dispose();

            return new PostDataObject()
            {
                TopicName = topic.Name,
                AuthorName = author.NickName,
                Content = post.Content,
                CreationDateTime = post.CreationDateTime
            };
        }

        /// <summary>
        /// 用 异步Domain Event handler 来保存Post到数据, 
        /// 用异步保存的同时 Post 的传输对象 PostDataObject返回 (加快速度,不需要等待数据库保存完成再返回)
        /// </summary>
        private PostDataObject PublishPostWithAsyncDomainEventHandler(int topicId, int authorId, string content)
        {
            //可从 分布式缓存中读取
            Topic topic = this.topicRepository.FindByKey(topicId);

            //可从 分布式缓存中读取
            User author = this.userRepository.FindByKey(authorId);

            Post post = Post.Create(topic, author, content);

            //用 异步Domain Event Handler 来 保存
            DomainEventAggregator.Instance.Publish<PostDomainEvent>
                (
                    new PostDomainEvent(post)
                    {
                        Post = post
                    }
                );

            return new PostDataObject()
            {
                TopicName = topic.Name,
                AuthorName = author.NickName,
                Content = post.Content,
                CreationDateTime = post.CreationDateTime
            };
        
        }


    }
}
