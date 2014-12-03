using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace EApp.Infrastructure.DomainEvent
{
    public interface IDomainEvent
    {
        IEntity Source { set; }

        TimeSpan Time { get; set; }
    }
}
