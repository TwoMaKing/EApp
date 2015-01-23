using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Core.DomainDriven.Bus
{
    public class MessageDispatcherFactory
    {
        public static IMessageDispatcher Create(IHandlerProvider handlerProvider, 
                                                string dispatcherName, 
                                                params object[] args)
        {
            IMessageDispatcher messageDispatcher = ServiceLocator.Instance.GetService<IMessageDispatcher>(dispatcherName);

            IEnumerable handlers = handlerProvider.GetHandlers();

            MethodInfo methodInfo = messageDispatcher.GetType().GetMethod("Register", BindingFlags.Public | BindingFlags.Instance);

            foreach (object handlerObject in handlers)
            {
                var handlerInterfaceTypeQuery = from p in handlerObject.GetType().GetInterfaces()
                                                where p.IsGenericType &&
                                                p.GetGenericTypeDefinition().Equals(typeof(IHandler<>))
                                                select p;

                if (handlerInterfaceTypeQuery != null)
                {
                    foreach (var handlerInterfaceType in handlerInterfaceTypeQuery) 
                    {
                        Type messageType = handlerInterfaceType.GetGenericTypeDefinition();

                        MethodInfo genericRegisterMethod = methodInfo.MakeGenericMethod(messageType);

                        genericRegisterMethod.Invoke(messageDispatcher, new object[] { handlerObject });
                    }
                }
            }

            return messageDispatcher;
        }
    }
}
