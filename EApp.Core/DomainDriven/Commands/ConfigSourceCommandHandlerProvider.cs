using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;

namespace EApp.Core.DomainDriven.Commands
{
    public class ConfigSourceCommandHandlerProvider : ICommandHandlerProvider
    {
        public IDictionary<Type, Type> GetCommandHandlers()
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

                    var commandHandlerInterfaceQuery = from c in handlerType.GetInterfaces()
                                                       where c.IsGenericType &&
                                                             c.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                                                       select c;      

                    foreach (var commandHandlerInterface in commandHandlerInterfaceQuery)
                    {
                        Type commandType = commandHandlerInterface.GetGenericTypeDefinition();

                        commandHandlerDictionary.Add(commandType, handlerType);
                    }
                }
            }

            return commandHandlerDictionary;
        }
    }
}
