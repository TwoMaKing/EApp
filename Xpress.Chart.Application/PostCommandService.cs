using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.LightBus;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Commands;
using Xpress.Chart.Domain.Events;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;
using Xpress.Chart.Repositories;
using Xpress.Chart.ServiceContracts;

namespace Xpress.Chart.Application
{
    public class PostCommandService : IPostCommandService
    {
        public void PublishPost(PostDataObject post)
        {
            PostPublishCommand command = new PostPublishCommand();
          
            command.TopicId = post.Topic.Id;
            command.AuthorId = post.Author.Id;
            command.Content = post.Content;

            this.ExecuteCommand(command);
        }

        private void ExecuteCommand(ICommand command) 
        {
            using (ICommandBus commandBus = ServiceLocator.Instance.GetService<ICommandBus>())
            {
                commandBus.Publish(command);

                commandBus.Commit();
            }
        }
    }
}
