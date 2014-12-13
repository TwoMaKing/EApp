using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Events;

namespace EApp.Infrastructure.Domain.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        private Guid id = Guid.NewGuid();

        private DateTime timestamp = DateTime.UtcNow;

        private IEntity source;

        public DomainEvent(IEntity source)
        {
            this.source = source;
        }

        public IEntity Source
        {
            get 
            {
                return this.source;
            }
        }

        public Guid Id
        {
            get 
            {
                return this.id;
            }
        }

        public DateTime TimeStamp
        {
            get 
            {
                return this.timestamp;
            }
        }
    }
}
