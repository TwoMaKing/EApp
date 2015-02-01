using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core;
using EApp.Core.Application;
using EApp.Domain.Core.Bus;
using EApp.Domain.Core.Commands;
using Xpress.Chat.Commands;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Events;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;
using Xpress.Chat.Repositories;
using Xpress.Chat.ServiceContracts;

namespace Xpress.Chat.Application
{
    public class PostCommandService : IPostCommandService
    {
        public void PublishPost(PostDataObject post)
        {
            PostPublishCommand command = new PostPublishCommand();
          
            command.TopicId = post.Topic.Id;
            command.AuthorId = post.Author.Id;
            command.Content = post.Content;

            command.PostDataObject = post;

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
