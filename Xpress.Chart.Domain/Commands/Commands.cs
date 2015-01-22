using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Commands;

namespace Xpress.Chart.Domain.Commands
{
    public class PostPublishCommand : Command
    {
        public int TopicId { get; set; }

        public int AuthorId { get; set; }

        public string Content { get; set; }

    }
}
