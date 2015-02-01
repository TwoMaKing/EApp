using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;

namespace EApp.Domain.Core.Commands
{
    public class ConfigSourceCommandHandlerProvider : ICommandHandlerProvider
    {
        public IDictionary<Type, Type> GetHandlers()
        {
            IDictionary<Type, Type> commandHandlerDictionary = new Dictionary<Type, Type>();

            HandlerElementCollection handlerElements = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Handlers;

            if (handlerElements != null &&
                handlerElements.Count > 0)
            {
                for (int handlerIndex = 0; handlerIndex < handlerElements.Count; handlerIndex++)
                {
                    HandlerElement handlerElement = handlerElements[handlerIndex];

                    string handlerName = handlerElement.Name;

                    string handlerTypeName = handlerElement.Type;

                    Type handlerType = Type.GetType(handlerTypeName);

                    var commandHandlerInterfaceQueryable = from commandHandlerInterface in handlerType.GetInterfaces()
                                                           where commandHandlerInterface.IsGenericType &&
                                                                 commandHandlerInterface.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                                                           select commandHandlerInterface;

                    foreach (var commandHandlerInterface in commandHandlerInterfaceQueryable)
                    {
                        Type commandType = commandHandlerInterface.GetGenericArguments().FirstOrDefault();

                        if (typeof(ICommand).IsAssignableFrom(commandType))
                        {
                            commandHandlerDictionary.Add(commandType, handlerType);
                        }
                    }
                }
            }

            return commandHandlerDictionary;
        }
    }
}
