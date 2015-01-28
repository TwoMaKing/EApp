using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Repository;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;

namespace Xpress.Chat.Commands
{
    public class PostPublishCommandHandler : ICommandHandler<PostPublishCommand>
    {
        public void Handle(PostPublishCommand message)
        {
            //Post post = message.PostDataObject.MapTo();
            //post.Publish();

            using (IRepositoryContext repositoryContext = ServiceLocator.Instance.GetService<IRepositoryContext>())
            {
                IPostRepository postRepository = (IPostRepository)repositoryContext.GetRepository<Post>();

                Post post = message.PostDataObject.MapTo();

                postRepository.Add(post);

                repositoryContext.Commit();
            }
        }
    }
}
