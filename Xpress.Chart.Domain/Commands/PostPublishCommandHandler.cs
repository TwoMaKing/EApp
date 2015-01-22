using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Repository;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;

namespace Xpress.Chart.Domain.Commands
{
    public class PostPublishCommandHandler : ICommandHandler<PostPublishCommand>
    {
        public void Handle(PostPublishCommand message)
        {
            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IPostRepository postRepository = (IPostRepository)repositoryContext.GetRepository<Post>();

                Post post = Post.Create(null, null, message.Content);

                postRepository.Add(post);

                repositoryContext.Commit();
            }
        }
    }
}
