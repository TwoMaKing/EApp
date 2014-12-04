using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Infrastructure.Domain;

namespace EApp.Infrastructure.DomainEvent
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent() 
        { 
            
        }

        public IEntity Source
        {
            set { throw new NotImplementedException(); }
        }

        public TimeSpan Time
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
