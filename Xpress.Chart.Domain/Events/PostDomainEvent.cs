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
    public class PostDomainEvent : DomainEvent
    {
        public PostDomainEvent(IEntity source) : base(source) { }

        public Post Post { get; set; }
    }
}
