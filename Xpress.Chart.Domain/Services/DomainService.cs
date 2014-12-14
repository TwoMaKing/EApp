using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Domain.Events;
using EApp.Infrastructure.Events;
using EApp.Infrastructure.Repository;
using EApp.Infrastructure.UnitOfWork;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using EApp.Repositories.SqlServer;

namespace Xpress.Chart.Domain.Services
{
    public class DomainService : IDomainService
    {
        private IRepositoryContext repositoryContext;
        private IRepository<Post> postRepository;

        public DomainService(
            IRepositoryContext repositoryContext,
            IRepository<Post> postRepository) 
        {
            this.repositoryContext = repositoryContext;
            this.postRepository = postRepository;
            

        }

        public Post PublishPost(Topic topic, User author, string content)
        {
            Post post = Post.Create(topic, author, content);

            postRepository.Add(post);

            repositoryContext.Commit();

            return post;
        }

        public void PublishComment(Comment comment, Post post)
        {
            throw new NotImplementedException();
        }

    }
}
