using EApp.Core.DomainDriven.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace EApp.Core.DomainDriven.Bus
{
    /// <summary>
    /// CQRS command bus. We can use command bus to publish command.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private ThreadLocal<Queue<ICommand>> commandQueue = new ThreadLocal<Queue<ICommand>>(() => new Queue<ICommand>());

        private ThreadLocal<bool> committed = new ThreadLocal<bool>(() => true);

        private Guid id = Guid.NewGuid();

        private MethodInfo publishMethod;

        public Guid Id
        {
            get 
            { 
                return this.id; 
            }
        }

        public void Publish<TMessage>(TMessage message)
        {
            if (!(message is ICommand))
            {
                throw new ArgumentException("");
            }

            this.commandQueue.Value.Enqueue((ICommand)message);
        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages)
        {
            foreach(TMessage message in messages)
            {
                this.Publish<TMessage>(message);
            }
        }

        public void Clear()
        {
            this.commandQueue.Value.Clear();
        }

        public bool Committed
        {
            get 
            { 
                return this.committed.Value; 
            }
        }

        public void Commit()
        {
            while (this.commandQueue.Value.Count > 0)
            {

            }

            this.committed.Value = true;
        }

        public void Rollback()
        {
            this.Clear();
        }

        public void Dispose()
        {
            this.Clear();
            this.commandQueue.Dispose();
        }
    }
}
