using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Domain.Core.Repositories;
using EApp.Repositories.SQL;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Domain.Services
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
