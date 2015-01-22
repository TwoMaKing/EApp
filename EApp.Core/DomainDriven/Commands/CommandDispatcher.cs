using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Core.DomainDriven;
using EApp.Core.Exceptions;
using Microsoft.Practices.Unity;

namespace EApp.Core.DomainDriven.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private static IDictionary<Type, Type> commandHandlerTypes;

        private readonly Dictionary<Type, List<object>> commandHandlers = new Dictionary<Type, List<object>>();

        public static ICommandDispatcher Configure(ICommandHandlerProvider commandHandlerProvider,
                                                   string dispatcherName) 
        {
            commandHandlerTypes = commandHandlerProvider.GetCommandHandlers();

            ICommandDispatcher commandDispatcher = EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve<ICommandDispatcher>(dispatcherName);

            if (commandHandlerTypes != null)
            {
                MethodInfo registerMethod = commandDispatcher.GetType().GetMethod("Register", BindingFlags.Instance | BindingFlags.Public);

                foreach (KeyValuePair<Type, Type> commandHandlerPair in commandHandlerTypes)
                {
                    if (!EAppRuntime.Instance.CurrentApp.ObjectContainer.Registered(commandHandlerPair.Value))
                    {
                        EAppRuntime.Instance.CurrentApp.ObjectContainer.RegisterType(commandHandlerPair.Value);
                    }

                    MethodInfo genericRegisterMethod = registerMethod.MakeGenericMethod(commandHandlerPair.Key);

                    genericRegisterMethod.Invoke(commandDispatcher,
                                                 new object[] { EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(commandHandlerPair.Value) });
                }
            }

            return commandDispatcher;
        }

        public void Clear()
        {
            this.commandHandlers.Clear();
        }

        public void Register<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : class, ICommand
        {
            Type commandType = typeof(TCommand);

            if (!this.commandHandlers.ContainsKey(commandType))
            {
                this.commandHandlers.Add(commandType, new List<object>());
            }

            if (this.commandHandlers[commandType] == null)
            {
                this.commandHandlers[commandType] = new List<object>();
            }

            if (!this.commandHandlers[commandType].Contains(commandHandler))
            {
                this.commandHandlers[commandType].Add(commandHandler);
            }
        }

        public void UnRegister<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : class, ICommand
        {
            Type commandType = typeof(TCommand);

            if (this.commandHandlers.ContainsKey(commandType) &&
                this.commandHandlers[commandType] != null &&
                this.commandHandlers[commandType].Count > 0)
            {
                this.commandHandlers[commandType].Remove(commandHandler);
            }
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            Type commandType = typeof(TCommand);

            if (this.commandHandlers.ContainsKey(commandType))
            {
                var handlerList = this.commandHandlers[commandType];

                foreach (var handler in handlerList)
                {
                    if (!(handler is ICommandHandler<TCommand>))
                    {
                        throw new InfrastructureException("The handler {0} is not a Command Handler.", handler.GetType().Name);
                    }

                    var dynamicHandler = (ICommandHandler<TCommand>)handler;

                    dynamicHandler.Handle(command);
                }
            }
        }

        public ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : class, ICommand
        {
            if (commandHandlers == null)
            {
                return null;
            }

            Type commandType = typeof(TCommand);

            if (commandHandlers.ContainsKey(commandType))
            {
                return (ICommandHandler<TCommand>)commandHandlers[commandType].FirstOrDefault();
            }

            return null;
        }
    }
}
