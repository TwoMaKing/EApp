using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EApp.Domain.Core.Commands;

namespace EApp.Domain.Core.Bus
{
    public class CommandBus : ICommandBus
    {
        private ThreadLocal<Queue<ICommand>> commandQueue = new ThreadLocal<Queue<ICommand>>(() => new Queue<ICommand>());

        private ICommand[] backupCommands;

        private ThreadLocal<bool> committed = new ThreadLocal<bool>();

        private ICommandDispatcher commandDispatcher;

        private MethodInfo dispatchMethod;

        public CommandBus(ICommandDispatcher commandDispatcher) 
        {
            this.commandDispatcher = commandDispatcher;

            this.dispatchMethod = this.commandDispatcher.GetType().GetMethod("Dispatch", BindingFlags.Public | BindingFlags.Instance);
        }

        public bool DistributedTransactionSupported
        {
            get 
            { 
                return false; 
            }
        }

        public bool Committed
        {
            get
            {
                return this.committed.Value;
            }
        }

        public void Clear()
        {
            this.commandQueue.Value.Clear();
        }

        public void Publish<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            commandQueue.Value.Enqueue(command);
            committed.Value = false;
        }

        public void Publish<TCommand>(IEnumerable<TCommand> commands) where TCommand : class, ICommand
        {
            if (commands != null)
            {
                foreach (TCommand command in commands) 
                {
                    this.Publish<TCommand>(command);
                }
            }
        }

        public void Commit()
        {
            this.backupCommands = new ICommand[this.commandQueue.Value.Count];

            this.commandQueue.Value.CopyTo(this.backupCommands, 0);

            while (this.commandQueue.Value.Count > 0)
            {
                ICommand command = this.commandQueue.Value.Dequeue();

                Type commandType = command.GetType();

                MethodInfo genericDispatchMethod = this.dispatchMethod.MakeGenericMethod(commandType);

                genericDispatchMethod.Invoke(this.commandDispatcher, new object[] { command });
            }

            this.committed.Value = true;
        }

        public void Rollback()
        {
            if (this.backupCommands != null &&
                this.backupCommands.Length > 0)
            {
                this.Clear();

                foreach (ICommand command in this.backupCommands)
                {
                    this.commandQueue.Value.Enqueue(command);
                }
            }

            this.committed.Value = false;
        }

        public void Dispose()
        {
            this.Clear();
            this.commandQueue.Dispose();
            this.committed.Dispose();
        }
    }
}
