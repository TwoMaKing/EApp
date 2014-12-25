using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Domain.Events;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Repositories.SqlServer;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;

namespace Xpress.Chart.Domain.Services
{
    public class DomainService : IDomainService
    {
        private IRepositoryContext repositoryContext;
        private IPostRepository postRepository;
        private ICommentRepository commentRepository;

        public DomainService(IRepositoryContext repositoryContext,
                             IPostRepository postRepository,
                             ICommentRepository commentRepository) 
        {
            this.repositoryContext = repositoryContext;
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;

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
