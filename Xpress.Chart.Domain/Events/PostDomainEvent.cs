using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Domain.Events;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;

namespace Xpress.Chat.Domain.Events
{
    public class PostDomainEvent : DomainEvent
    {
        public PostDomainEvent(IEntity source) : base(source) { }

        public Post Post { get; set; }
    }
}
