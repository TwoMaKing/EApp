using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Events
{
    public class ActionDelegateEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        private Action<TEvent> actionDelegate;

        public ActionDelegateEventHandler(Action<TEvent> actionDelegate) 
        {
            this.actionDelegate = actionDelegate;
        }

        public void Handle(TEvent t)
        {
            this.actionDelegate(t);
        }

        public override int GetHashCode()
        {
            if (this.actionDelegate == null)
            {
                return base.GetHashCode();
            }

            return this.actionDelegate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!object.ReferenceEquals(this, obj))
            {
                return false;
            }

            if (this.actionDelegate == null ||
                obj == null)
            {
                return false;
            }

            if (!(obj is ActionDelegateEventHandler<TEvent>))
            {
                return false;
            }

            return Delegate.ReferenceEquals(this.actionDelegate, (ActionDelegateEventHandler<TEvent>)obj);
        }

    }
}
