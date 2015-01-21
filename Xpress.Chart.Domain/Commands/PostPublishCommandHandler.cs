using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Commands;

namespace Xpress.Chart.Domain.Commands
{
    public class PostPublishCommandHandler : ICommandHandler<PostPublishCommand>
    {
        public void Handle(PostPublishCommand message)
        {
            // to save.
        }
    }
}
