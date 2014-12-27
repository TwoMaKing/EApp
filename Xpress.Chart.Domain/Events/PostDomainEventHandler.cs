using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Domain.Events;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;

namespace Xpress.Chart.Domain.Events
{
    public class PostDomainEventHandler : IDomainEventHandler<PostDomainEvent>
    {
        private IRepositoryContext repositoryContext;

        private IPostRepository postRepository;

        public PostDomainEventHandler(
            IRepositoryContext repositoryContext,
            IPostRepository postRepository) 
        {
            this.repositoryContext = repositoryContext;
            this.postRepository = postRepository;
        }

        public void Handle(PostDomainEvent t)
        {
            if (t != null &&
                t.Post != null)
            {
                this.postRepository.Add(t.Post);

                this.repositoryContext.Commit();

                //好想用Using 能自动Dispose.
                this.repositoryContext.Dispose();
            }
        }
    }
}
